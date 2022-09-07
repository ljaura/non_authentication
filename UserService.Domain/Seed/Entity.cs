
namespace UserService.Domain.Seed
{

    public abstract class Entity<T>
    {
        public virtual T? Id { get; set; }

        public Guid Guid { get; set; }
    }

}