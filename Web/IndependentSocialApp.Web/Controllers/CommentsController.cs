namespace IndependentSocialApp.Web.Controllers
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.Extensions;
    using IndependentSocialApp.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : ApiController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCommentResponseModel>> Create(CreateCommentRequestModel model)
        {
            var userId = this.User.GetId();

            var commentModel = await this.commentsService.CreateAsync(model, userId);

            return this.CreatedAtAction(nameof(this.GetById), new { id = commentModel.Id }, commentModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return this.Ok();
        }

    }
}
