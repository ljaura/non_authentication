using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UserService.Domain.Entities;
using UserService.Domain.Seed;
using UserService.Infrastructure.EntityConfiguration;
using System.Data;

namespace UserService.Infrastructure.Configuration
{
    public class UserContext : DbContext, IUnitOfWork
    {
        public DbSet<UserAddress> UserAddresses { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        private IDbContextTransaction? _currentTransaction;

        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAddressEntityTypeConfiguration());
          
            //seed temp data required 
            modelBuilder.Seed();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction?> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }


    }
}
