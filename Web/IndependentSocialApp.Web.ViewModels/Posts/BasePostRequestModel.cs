namespace IndependentSocialApp.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using static IndependentSocialApp.Common.ModelValidations.Post;

    public abstract class BasePostRequestModel
    {
        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
