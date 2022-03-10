namespace IndependentSocialApp.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : ApiController
    {
        private readonly IPostsService _postsService;
        private readonly IMapper _mapper;

        public PostsController(
            IPostsService postsService,
            IMapper mapper)
        {
            this._postsService = postsService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CreatePostRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var post = await this._postsService.CreateAsync(model, userId);

            var postResponseModel = this._mapper.Map<PostResponseModel>(post);

            return this.CreatedAtAction(nameof(this.GetPost), new { id = post.Id }, postResponseModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseModel>> GetPost(int id)
        {
            var post = await this._postsService.GetByIdAsync<PostResponseModel>(id);

            return Ok(post);
        }
    }
}
