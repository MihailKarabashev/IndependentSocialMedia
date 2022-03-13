namespace IndependentSocialApp.Services.Data
{
    using System;
    using System.Collections.Generic;
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

        public async Task<PostResponseModel> CreateAsync(CreatePostRequestModel model, string userId)
        {
            var post = new Post
            {
                Description = model.Description,
                ApplicationUserId = userId,
                ImageUrl = model.ImageUrl,
            };

            await this._postsRepository.AddAsync(post);
            await this._postsRepository.SaveChangesAsync();

            var mappedPostModel = AutoMapperConfig.MapperInstance.Map<PostResponseModel>(post);

            return mappedPostModel;
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var post = this.ValidateUserPostCredentials(id, userId);

            post.ModifiedOn = DateTime.UtcNow;
            post.IsDeleted = true;

            this._postsRepository.Update(post);
            await this._postsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string userId, UpdatePostRequestModel model)
        {
            var post = this.ValidateUserPostCredentials(id, userId);

            post.ImageUrl = model.ImageUrl;
            post.Description = model.Description;

            this._postsRepository.Update(post);
            await this._postsRepository.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this._postsRepository
                 .AllAsNoTracking()
                 .Where(x => !x.IsDeleted)
                 .To<T>()
                 .ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this._postsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id && !x.IsDeleted)
                .To<T>()
                .FirstOrDefaultAsync() ?? throw new NotFoundException(PostNotFound);
        }

        private Post ValidateUserPostCredentials(int id, string userId)
        {
            var post = this.FindByPostByandByUserId(id);

            if (post == null)
            {
                throw new NotFoundException(PostNotFound);
            }

            var isOwnerofPost = post.ApplicationUserId == userId;

            if (!isOwnerofPost)
            {
                throw new NoPermissionException(NotPostOwner);
            }

            return post;
        }

        private Post FindByPostByandByUserId(int id)
             => this._postsRepository
              .AllAsNoTracking()
              .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
    }
}
