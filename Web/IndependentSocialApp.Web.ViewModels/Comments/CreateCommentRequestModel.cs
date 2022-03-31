namespace IndependentSocialApp.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static IndependentSocialApp.Common.ModelValidations.Comment;

    public class CreateCommentRequestModel
    {
        [Required]
        public int PostId { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [MaxLength(ContentMaxLenght)]
        public string Content { get; set; }
    }
}
