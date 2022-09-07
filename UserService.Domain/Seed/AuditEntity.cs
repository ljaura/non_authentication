
namespace UserService.Domain.Seed
{

    public abstract class AuditEntity<T> : Entity<T>
    {
        private DateTime? _created;

        public DateTime CreatedDate
        {
            get
            {
                if (!_created.HasValue)
                    _created = DateTime.Now;

                return _created.Value;
            }
            set
            {
                _created = value;
            }
        }

        public long CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public long? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
