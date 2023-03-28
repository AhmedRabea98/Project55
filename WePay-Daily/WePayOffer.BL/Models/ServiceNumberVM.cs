namespace WePayOffer.BL.Models
{
    public class ServiceNumberVM
    {

        public ServiceNumberVM()
        {
            CreationDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string OfferId { get; set; }
        public DateTime CreationDate { get; set; }
        public int StatusId { get; set; }
    }
}
