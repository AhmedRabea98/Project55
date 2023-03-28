using WePayOffer.BL.Interface;
using WePayOffer.DAL.Database;
using WePayOffer.DAL.Entity;

namespace WePayOffer.BL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationContext Context;

        public UnitOfWork(ApplicationContext Context)
        {
            this.Context = Context;
        }



        // Service Number
        private GenericRep<ServiceNumber> serviceNumberRepository;
        public GenericRep<ServiceNumber> ServiceNumberRepository
        {
            get
            {
                if (this.serviceNumberRepository == null)
                {
                    this.serviceNumberRepository = new GenericRep<ServiceNumber>(Context);
                }
                return serviceNumberRepository;
            }
        }

        // Transaction
        private GenericRep<ServiceTransaction> serviceTransactionRepository;
        public GenericRep<ServiceTransaction> ServiceTransactionRepository
        {
            get
            {
                if (this.serviceTransactionRepository == null)
                {
                    this.serviceTransactionRepository = new GenericRep<ServiceTransaction>(Context);
                }
                return serviceTransactionRepository;
            }
        }
        public virtual async Task<bool> SaveAsync()
        {
            bool returnValue = true;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    await Context.SaveChangesAsync();
                   dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                   dbContextTransaction.Rollback();
                }
            }

            return returnValue;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
