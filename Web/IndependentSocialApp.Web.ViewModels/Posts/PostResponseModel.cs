namespace IndependentSocialApp.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using AutoMapper;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Mapping;

    public class PostResponseModel : IMapFrom<Post> , IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string PostUserId { get; set; }

        public IEnumerable<PostCommentModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostResponseModel>()
                  .ForMember(x => x.PostUserId, y => y.MapFrom(x => x.ApplicationUserId));
        }
    }
}
