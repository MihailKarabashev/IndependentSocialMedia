namespace IndependentSocialApp.Data.Configurations
{
    using IndependentSocialApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder
                 .HasOne(b => b.ApplicationUser)
                 .WithMany()
                 .HasForeignKey(f => f.ApplicationUserId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Comment)
                .WithMany(c => c.Likes)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
