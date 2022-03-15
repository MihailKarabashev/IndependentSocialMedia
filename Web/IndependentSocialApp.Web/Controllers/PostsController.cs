namespace IndependentSocialApp.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.Extensions;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static IndependentSocialApp.Common.NloggerMessages;

    public class PostsController : ApiController
    {
        private readonly IPostsService _postsService;
        private readonly INloggerManager _nlog;

        public PostsController(
            IPostsService postsService,
            INloggerManager nlog)
        {
            this._postsService = postsService;
            this._nlog = nlog;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] CreatePostRequestModel model)
        {
            var userId = this.User.GetId();

            var mappedModel = await this._postsService.CreateAsync(model, userId);

            this._nlog.LogInfo(string.Format(SuccesfullyCreated, mappedModel.Id));

            return this.CreatedAtAction(nameof(this.GetPost), new { id = mappedModel.Id }, mappedModel);
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
        public async Task<ActionResult<AllPostResponseModel>> GetAllPosts([FromQuery] PostParams model)
        {
            var allPost = new AllPostResponseModel
            {
                Posts = await this._postsService.GetAllAsync<PostResponseModel>(model),
            };

            return allPost;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var userId = this.User.GetId();

            await this._postsService.DeleteAsync(id, userId);

            this._nlog.LogInfo(string.Format(SuccesfullyRemoved, id));

            return this.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePostRequestModel model)
        {
            var userId = this.User.GetId();

            await this._postsService.EditAsync(id, userId, model);

            this._nlog.LogInfo(string.Format(SuccesfullyEdited, id));

            return this.NoContent();
        }
    }
}
