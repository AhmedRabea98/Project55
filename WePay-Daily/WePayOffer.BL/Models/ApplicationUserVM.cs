namespace WePayOffer.BL.Models
{
    public class ApplicationUserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
