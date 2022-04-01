namespace IndependentSocialApp.Data.Configurations
{
    using IndependentSocialApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder
                .HasOne(a => a.Addressee)
                .WithMany(f => f.FriendshipAdressee)
                .HasForeignKey(a => a.AddresseeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                 .HasOne(r => r.Requester)
                 .WithMany(f => f.FriendShipRequester)
                 .HasForeignKey(r => r.RequesterId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
