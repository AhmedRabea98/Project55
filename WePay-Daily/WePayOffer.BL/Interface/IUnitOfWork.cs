namespace WePayOffer.BL.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}
