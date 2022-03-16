namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Common.ExecptionFactory.Others;
    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Mapping;
    using IndependentSocialApp.Web.ViewModels.Likes;

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

            var comment = await this.commentsService.GetByIdAsync<Comment>(model.Id);

            if (comment == null)
            {
                throw new NotFoundException(CommentNotFound);
            }

            var like = new Like()
            {
                ApplicationUserId = user.Id,
                CommentId = comment.Id,
                IsLike = true,
            };

            await this.likesRepo.AddAsync(like);
            await this.likesRepo.SaveChangesAsync();
        }

        public async Task CreatePostLikeAsync(LikeRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            var post = await this.postsService.GetByIdAsync<Post>(model.Id);

            if (post == null)
            {
                throw new NotFoundException(PostNotFound);
            }

            var like = new Like()
            {
                ApplicationUserId = user.Id,
                PostId = post.Id,
                IsLike = true,
            };

            await this.likesRepo.AddAsync(like);
            await this.likesRepo.SaveChangesAsync();
        }
    }
}
