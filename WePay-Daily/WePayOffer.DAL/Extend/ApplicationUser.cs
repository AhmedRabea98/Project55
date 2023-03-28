using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePayOffer.DAL.Extend
{
    public class ApplicationUser : IdentityUser
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [ForeignKey("CreatedBy")]
        public ApplicationUser Users { get; set; }
        public ICollection<ApplicationUser> ListOfUsers { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
