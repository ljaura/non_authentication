using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Configuration
{
    internal static class SeedingDataContext
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var userAddress = new UserAddress("Test data");
            userAddress.Id = 1;
            userAddress.UserId = 1;
            userAddress.CreatedBy = -1;//created automatically

            modelBuilder.Entity<UserAddress>().HasData(userAddress);
        }
    }
}
