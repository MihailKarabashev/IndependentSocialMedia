namespace IndependentSocialApp.Web.ViewModels.Follows
{
    using System.ComponentModel.DataAnnotations;

    public class CreateFollowRequestModel
    {
        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public string FollowerId { get; set; }
    }
}
