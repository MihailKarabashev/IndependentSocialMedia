namespace IndependentSocialApp.Data.Configurations
{
    using IndependentSocialApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
           builder
                .HasOne(x => x.ApplicationUser)
                .WithMany()
                .HasForeignKey(f => f.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

           builder
                .HasOne(x => x.FollowerUser)
                .WithMany()
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
