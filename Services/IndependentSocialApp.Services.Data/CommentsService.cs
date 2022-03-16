using IndependentSocialApp.Common.ExecptionFactory.Others;
using IndependentSocialApp.Data.Common.Repositories;
using IndependentSocialApp.Data.Models;
using IndependentSocialApp.Services.Mapping;
using IndependentSocialApp.Web.Common.ExecptionFactory.Auth;
using IndependentSocialApp.Web.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static IndependentSocialApp.Common.ModelValidations.Identity;
using static IndependentSocialApp.Common.ModelValidations.Comment;
using static IndependentSocialApp.Common.ModelValidations.Post;
using System;

namespace IndependentSocialApp.Services.Data
{

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepo;
        private readonly IUsersService usersService;
        private readonly IPostsService postsService;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepo,
            IUsersService usersService,
            IPostsService postsService)
        {
            this.commentsRepo = commentsRepo;
            this.usersService = usersService;
            this.postsService = postsService;
        }

        public async Task<CommentResponseModel> CreateAsync(CreateCommentRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            var post = this.postsService.FindPostById(model.PostId);

            if (post == null)
            {
                throw new NotFoundException(PostNotFound);
            }

            var comment = new Comment
            {
                ApplicationUserId = user.Id,
                PostId = post.Id,
                Content = model.Content,
            };

            await this.commentsRepo.AddAsync(comment);
            await this.commentsRepo.SaveChangesAsync();

            var mappedCommentModel = AutoMapperConfig.MapperInstance.Map<CommentResponseModel>(comment);
            return mappedCommentModel;
        }

        public async Task DeleteOwnCommentAsync(int commentId, string userId)
        {
            var comment = await this.ValidateCommentCredentials(commentId, userId);

            comment.IsDeleted = true;
            comment.DeletedOn = DateTime.UtcNow;

            this.commentsRepo.Update(comment);
            await this.commentsRepo.SaveChangesAsync();
        }

        public async Task EditAsync(UpdateCommentRequestModel model, string userId , int id)
        {
            var comment = await this.ValidateCommentCredentials(id, userId);

            comment.Content = model.Content;
            comment.ModifiedOn = DateTime.UtcNow;

            this.commentsRepo.Update(comment);
            await this.commentsRepo.SaveChangesAsync();
        }

        private async Task<Comment> ValidateCommentCredentials(int commentId, string userId)
        {
            var comment = await this.commentsRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == commentId && !x.IsDeleted);

            if (comment == null)
            {
                throw new NotFoundException(CommentNotFound);
            }

            var user = await this.usersService.FindUserAsync(userId);

            if (comment.ApplicationUserId != user.Id)
            {
                throw new AuthUnAuthorizedException(UnauthorizedAccess);
            }

            return comment;
        }
    }
}
