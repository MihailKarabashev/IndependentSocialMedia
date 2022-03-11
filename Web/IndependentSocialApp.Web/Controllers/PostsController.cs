namespace IndependentSocialApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static IndependentSocialApp.Common.NLoggerExceptionMessages;

    public class PostsController : ApiController
    {
        private readonly IPostsService _postsService;
        private readonly IMapper _mapper;
        private readonly INloggerManager _nlog;

        public PostsController(
            IPostsService postsService,
            IMapper mapper,
            INloggerManager nlog)
        {
            this._postsService = postsService;
            this._mapper = mapper;
            this._nlog = nlog;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePostRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this._nlog.LogError(this.ModelState.ToString());
                return this.BadRequest(this.ModelState);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var post = await this._postsService.CreateAsync(model, userId);

            var postResponseModel = this._mapper.Map<PostResponseModel>(post);

            this._nlog.LogInfo(SuccesfullyCreated);

            return this.CreatedAtAction(nameof(this.GetPost), new { id = post.Id }, postResponseModel);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseModel>> GetPost(int id)
        {
            var post = await this._postsService.GetByIdAsync<PostResponseModel>(id);
            return this.Ok(post);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<AllPostResponseModel>> GetAllPosts()
        {
            var allPost = new AllPostResponseModel
            {
                Posts = await this._postsService.GetAllAsync<PostResponseModel>(),
            };

            return allPost;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await this._postsService.DeleteAsync(id, userId);

            if (!result)
            {
                this._nlog.LogError("Problem");
            }

            this._nlog.LogInfo("OK Removed");

            return this.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePostRequestModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this._postsService.EditAsync(id, userId, model);

            return this.NoContent();
        }
    }
}
