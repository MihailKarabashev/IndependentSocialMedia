namespace IndependentSocialApp.Web.ViewModels.Likes
{
    using System.ComponentModel.DataAnnotations;

    public class LikeRequestModel
    {
        [Required]
        public int Id { get; set; }
    }
}
