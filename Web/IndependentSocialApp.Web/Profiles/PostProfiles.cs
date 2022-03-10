namespace IndependentSocialApp.Web.Profiles
{
    using AutoMapper;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Posts;

    public class PostProfiles : Profile
    {
        public PostProfiles()
        {
            this.CreateMap<Post, PostResponseModel>();
        }
    }
}
