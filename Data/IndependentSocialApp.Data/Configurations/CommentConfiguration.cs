namespace IndependentSocialApp.Data.Configurations
{
    using IndependentSocialApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne(x => x.ApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
