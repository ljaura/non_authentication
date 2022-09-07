using UserService.Domain.Seed;

namespace UserService.Domain.Entities
{
    public class UserAddress : AuditEntity<int>
    {
        public UserAddress(string street)
        {
            Guid = Guid.NewGuid();
            Street = street;
        }

        public int UserId { get; set; }

        public string Street { get; set; }
    }
}
