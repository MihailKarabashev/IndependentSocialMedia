namespace IndependentSocialApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IndependentSocialApp.Data.Common.Models;

    using static IndependentSocialApp.Common.ModelValidations.Comment;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int? ParentId { get; set; }

        public Comment Parent { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        [MaxLength(ContentMaxLenght)]
        public string Content { get; set; }

        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    }
}
