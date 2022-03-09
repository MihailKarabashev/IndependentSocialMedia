namespace IndependentSocialApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IndependentSocialApp.Data.Common.Models;

    using static IndependentSocialApp.Common.ModelValidations.Post;

    public class Post : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
    }
}
