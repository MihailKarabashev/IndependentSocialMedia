namespace IndependentSocialApp.Web.ViewModels.Comments
{
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Mapping;

    public class CommentResponseModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }
    }
}
