using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePayOffer.DAL.Entity
{

    [Table("ServiceNumber")]
    public class ServiceNumber
    {

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Number { get; set; }

        [Required]
        public string OfferId { get; set; }

        public DateTime CreationDate { get; set; }

        public int StatusId { get; set; }
        public Status? Status { get; set; }

    }
}
