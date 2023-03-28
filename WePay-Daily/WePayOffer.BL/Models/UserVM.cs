using System.ComponentModel.DataAnnotations;

namespace WePayOffer.BL.Models
{
    public class UserVM
    {

        public UserVM()
        {
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "kindly insert TE user name !")]
        [MaxLength(50, ErrorMessage = "Min Len 50")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "kindly insert your full name !")]
        [MaxLength(150, ErrorMessage = "Min Len 150")]
        public string FullName { get; set; }

        public string? Mobile { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
