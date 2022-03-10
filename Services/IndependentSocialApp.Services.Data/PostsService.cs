namespace IndependentSocialApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using IndependentSocialApp.Common.ExecptionFactory.Others;
    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Mapping;
    using IndependentSocialApp.Web.ViewModels.Posts;
    using Microsoft.EntityFrameworkCore;
    using static IndependentSocialApp.Common.ModelValidations.Post;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> _postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this._postsRepository = postsRepository;
        }

        public async Task<Post> CreateAsync(CreatePostRequestModel model, string userId)
        {
            var post = new Post
            {
                Description = model.Description,
                ApplicationUserId = userId,
                ImageUrl = model.ImageUrl,
            };

            await this._postsRepository.AddAsync(post);
            await this._postsRepository.SaveChangesAsync();

            return post;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this._postsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync() ?? throw new NotFoundException(PostNotFound);
        }
    }
}
