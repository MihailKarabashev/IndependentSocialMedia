namespace IndependentSocialApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static IndependentSocialApp.Common.ModelValidations.Profile;

    public class Profile
    {
        [Key]
        [Required]
        public string UserId { get; set; }

        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        [MaxLength(BiograpyMaxLenght)]
        public string Biography { get; set; }

        public bool IsPrivate { get; set; }

        public Gender Gender { get; set; }

        public ICollection<Follow> Follows { get; set; } = new HashSet<Follow>();
    }
}
