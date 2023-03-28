namespace WePayOffer.Portal.Models
{
    public class ReturnMessage
    {
        public int FunctionId { get; set; }
        public string ReturnCode { get; set; }
        public string RequestId { get; set; }

        public string ReturnDescription { get; set; }
    }
}
