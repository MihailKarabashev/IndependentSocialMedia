namespace IndependentSocialApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using IndependentSocialApp.Common.ExecptionFactory.Others;
    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Likes;
    using Microsoft.EntityFrameworkCore;

    using static IndependentSocialApp.Common.ModelValidations.Comment;
    using static IndependentSocialApp.Common.ModelValidations.Post;

    public class LikesService : ILikesService
    {
        private readonly IRepository<Like> likesRepo;
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IPostsService postsService;

        public LikesService(
            IRepository<Like> likesRepo,
            IUsersService usersService,
            ICommentsService commentsService,
            IPostsService postsService)
        {
            this.likesRepo = likesRepo;
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.postsService = postsService;
        }

        public async Task CreateCommentLikeAsync(LikeRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            var comment = this.IsValidComment(model);

            var existedLike = comment.Likes.FirstOrDefault(x => x.CommentId == model.Id);

            if (existedLike != null)
            {
                existedLike.IsCommentLike = true;
                this.likesRepo.Update(existedLike);
            }
            else
            {
                var like = new Like()
                {
                    ApplicationUserId = user.Id,
                    CommentId = comment.Id,
                    IsCommentLike = true,
                };

                await this.likesRepo.AddAsync(like);
            }

            await this.likesRepo.SaveChangesAsync();
        }

        public async Task CreateCommentUnlikeAsync(LikeRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            var comment = this.IsValidComment(model);

            var like = await this.FindLike(user, comment.Id, default);

            like.IsCommentLike = false;

            this.likesRepo.Update(like);
            await this.likesRepo.SaveChangesAsync();
        }

        public async Task CreatePostLikeAsync(LikeRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            var post = this.IsValidPost(model);

            var existedLike = post.Likes.FirstOrDefault(x => x.PostId == model.Id);

            if (existedLike != null)
            {
                existedLike.IsPostLike = true;
                this.likesRepo.Update(existedLike);
            }
            else
            {
                var like = new Like()
                {
                    ApplicationUserId = user.Id,
                    PostId = post.Id,
                    IsPostLike = true,
                };

                await this.likesRepo.AddAsync(like);
            }

            await this.likesRepo.SaveChangesAsync();
        }

        public async Task CreatePostUnlikeAsync(LikeRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            var post = this.IsValidPost(model);

            var like = await this.FindLike(user, default, post.Id);

            like.IsPostLike = false;

            this.likesRepo.Update(like);
            await this.likesRepo.SaveChangesAsync();
        }

        private Post IsValidPost(LikeRequestModel model)
        {
            var post = this.postsService.FindPostById(model.Id);

            if (post == null)
            {
                throw new NotFoundException(PostNotFound);
            }

            return post;
        }

        private Comment IsValidComment(LikeRequestModel model)
        {
            var comment = this.commentsService.FindCommentById(model.Id);

            if (comment == null)
            {
                throw new NotFoundException(CommentNotFound);
            }

            return comment;
        }

        private async Task<Like> FindLike(ApplicationUser user, int commentId = 0, int postId = 0)
        {
            var like = commentId == 0
                     ? await this.likesRepo
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.PostId == postId && x.ApplicationUser == user)
                : await this.likesRepo
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.CommentId == commentId && x.ApplicationUser == user);

            if (like == null)
            {
                throw new NotFoundException(LikeBeforeUnlike);
            }

            return like;
        }
    }
}
