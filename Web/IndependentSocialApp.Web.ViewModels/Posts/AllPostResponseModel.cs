namespace IndependentSocialApp.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostResponseModel
    {
        public IEnumerable<PostResponseModel> Posts { get; set; }
    }
}
