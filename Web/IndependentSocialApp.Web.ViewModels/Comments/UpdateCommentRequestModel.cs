namespace IndependentSocialApp.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static IndependentSocialApp.Common.ModelValidations.Comment;

    public class UpdateCommentRequestModel
    {
        [Required]
        [MaxLength(ContentMaxLenght)]
        public string Content { get; set; }
    }
}
