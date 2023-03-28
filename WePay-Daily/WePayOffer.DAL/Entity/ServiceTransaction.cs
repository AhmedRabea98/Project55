using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePayOffer.DAL.Entity
{

    [Table("ServiceTransaction")]
    public class ServiceTransaction
    {

        [Key]
        public int Id { get; set; }

        public int ServiceNumberId { get; set; }
        public ServiceNumber ServiceNumber { get; set; }

        [Required]
        public bool IsSucceed { get; set; }
        public DateTime CreationDate { get; set; }
        public string FunctionId { get; set; }
        public string ResponseMessage { get; set; }
        public string RequestId { get; set; }

    }
}
