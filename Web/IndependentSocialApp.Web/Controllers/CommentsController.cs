namespace IndependentSocialApp.Web.Controllers
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.Extensions;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;

    using static IndependentSocialApp.Common.NloggerMessages;

    public class CommentsController : ApiController
    {
        private readonly ICommentsService commentsService;
        private readonly INloggerManager nlog;

        public CommentsController(
            ICommentsService commentsService,
            INloggerManager nlog)
        {
            this.commentsService = commentsService;
            this.nlog = nlog;
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponseModel>> Create(CreateCommentRequestModel model)
        {
            var userId = this.User.GetId();

            var commentModel = await this.commentsService.CreateAsync(model, userId);
            this.nlog.LogInfo(string.Format(SuccesfullyCreated, commentModel.Id));

            return this.CreatedAtAction(nameof(this.GetById), new { id = commentModel.Id }, commentModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var userId = this.User.GetId();

            await this.commentsService.DeleteOwnCommentAsync(id, userId);

            this.nlog.LogInfo(string.Format(SuccesfullyRemoved, id));

            return this.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(UpdateCommentRequestModel model ,int id)
        {
            var userId = this.User.GetId();

            await this.commentsService.EditAsync(model, userId , id);

            this.nlog.LogInfo(string.Format(SuccesfullyEdited, id));

            return this.NoContent();
        }
    }
}
