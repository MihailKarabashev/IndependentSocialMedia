namespace IndependentSocialApp.Web.Controllers
{
    using IndependentSocialApp.Services.Data;

    public class CommentsController : ApiController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
    }
}
