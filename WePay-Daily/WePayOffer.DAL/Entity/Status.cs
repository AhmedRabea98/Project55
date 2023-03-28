using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WePayOffer.DAL.Entity
{

    [Table("Status")]
    public class Status
    {

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<ServiceNumber> ServiceNumber { get; set; }
    }
}
