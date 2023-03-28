using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WePayOffer.BL.Interface;
using WePayOffer.DAL.Database;
using WePayOffer.DAL.Entity;

namespace WePayOffer.BL.Repository
{
    public class GenericRep<TEntity> : IGenericRep<TEntity> where TEntity : class
    {


        private readonly ApplicationContext db;
        private DbSet<TEntity> dbSet;

        public GenericRep(ApplicationContext db)
        {
            this.db = db;
            this.dbSet = db.Set<TEntity>();
        }


        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {

            var data = await dbSet.Where(filter).ToListAsync();
            return data.ToList();

        }

        public IQueryable<ServiceTransaction> GetInclude()
        {
            IQueryable<ServiceTransaction> data = db.ServiceTransaction.Include(a => a.ServiceNumber);
            return data;
        }

        public IQueryable<ServiceNumber> GetServiceNumber()
        {
            IQueryable<ServiceNumber> data = db.ServiceNumber;
            return data;
        }

        public IQueryable<ServiceTransaction> GetServiceTransaction()
        {
            IQueryable<ServiceTransaction> data = db.ServiceTransaction;
            return data;
        }
        public async Task<string> CreateBulkAsync(List<TEntity> obj)
        {
            await dbSet.AddRangeAsync(obj);
            return "Data Saved";
        }

        public async Task<string> CreateTransactionAsync(TEntity entity)
        {
            await dbSet.AddRangeAsync(entity);

            return "Data Saved";
        }

        public string UpdateStatus(int id, int statusId)
        {

            var old = db.ServiceNumber.Find(id);

            old.StatusId = statusId;

            return "Done Updated";
        }

        public string GetRequestId()
        {
            return Guid.NewGuid().ToString();
            //int Count = 10000;
            //var Data = db.ServiceTransaction.LastOrDefault();
            //var obj = db.ServiceTransaction.FirstOrDefault(x => x.RequestId == null);

            ////var Data = db.ServiceTransaction.Count();

            //if (Data == null)
            //{
            //    obj.RequestId = "BSI" + "10000";
            //}

            //else
            //{
            //    //var x = Data.RequestId;

            //    obj.RequestId = "BSI" + (Data.Id + 1).ToString();
            //}
            //// olddata.RequestId = "BSI_" + (olddata.RequestId + 1);

            //return obj;

        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {

            int data = 0;

            if (filter != null)
            {
                data = await dbSet.Where(filter).CountAsync();
            }
            else
            {
                data = await dbSet.CountAsync();
            }

            return data;
        }
    }
}