using WePayOffer.DAL.Entity;

namespace WePayOffer.BL.Models
{
    public class ServiceTransactionVM
    {

        public int Id { get; set; }
        public int ServiceNumberId { get; set; }
        public ServiceNumber ServiceNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSucceed { get; set; }
        public string ResponseMessage { get; set; }
        public string FunctionId { get; set; }
        public string RequestId { get; set; }
    }
}
