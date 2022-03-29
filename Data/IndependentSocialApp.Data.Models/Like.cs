namespace IndependentSocialApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IndependentSocialApp.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int? PostId { get; set; }

        public Post Post { get; set; }

        public int? CommentId { get; set; }

        public Comment Comment { get; set; }

        public bool IsCommentLike { get; set; }

        public bool IsPostLike { get; set; }
    }
}
