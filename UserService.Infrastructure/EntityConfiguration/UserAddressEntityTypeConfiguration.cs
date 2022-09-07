using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.EntityConfiguration
{
    internal class UserAddressEntityTypeConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable(nameof(UserAddress));

            builder.HasKey(x => x.Id);
        }
    }
}
