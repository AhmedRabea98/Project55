using System.Linq.Expressions;
using WePayOffer.DAL.Entity;

namespace WePayOffer.BL.Interface
{
    public interface IGenericRep<TEntity> where TEntity : class
    {

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);
        IQueryable<ServiceTransaction> GetInclude();
        IQueryable<ServiceNumber> GetServiceNumber();
        IQueryable<ServiceTransaction> GetServiceTransaction();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<string> CreateBulkAsync(List<TEntity> obj);
        Task<string> CreateTransactionAsync(TEntity obj);
        string UpdateStatus(int id, int statusId);
        public string GetRequestId();

    }
}
