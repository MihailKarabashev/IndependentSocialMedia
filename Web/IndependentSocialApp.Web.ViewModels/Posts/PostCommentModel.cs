namespace IndependentSocialApp.Web.ViewModels.Posts
{
    using AutoMapper;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Mapping;

    public class PostCommentModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CommentUserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, PostCommentModel>()
                 .ForMember(x => x.CommentUserId, y => y.MapFrom(x => x.ApplicationUserId));
        }
    }
}
