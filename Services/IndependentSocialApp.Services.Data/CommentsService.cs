using IndependentSocialApp.Common.ExecptionFactory.Others;
using IndependentSocialApp.Data.Common.Repositories;
using IndependentSocialApp.Data.Models;
using IndependentSocialApp.Services.Mapping;
using IndependentSocialApp.Web.Common.ExecptionFactory.Auth;
using IndependentSocialApp.Web.ViewModels.Comments;
using System.Threading.Tasks;
using static IndependentSocialApp.Common.ModelValidations.Identity;
using static IndependentSocialApp.Common.ModelValidations.Post;

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

        public async Task<CreateCommentResponseModel> CreateAsync(CreateCommentRequestModel model, string userId)
        {
            var user = await this.usersService.FindUserAsync(userId);

            if (user == null)
            {
                throw new AuthUnAuthorizedException(UnauthorizedAccess);
            }

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

            var mappedCommentModel = AutoMapperConfig.MapperInstance.Map<CreateCommentResponseModel>(comment);
            return mappedCommentModel;
        }
    }
}
