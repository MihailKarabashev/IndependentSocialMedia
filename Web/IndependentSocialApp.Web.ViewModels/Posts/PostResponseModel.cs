namespace IndependentSocialApp.Web.ViewModels.Posts
{
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Mapping;

    public class PostResponseModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
