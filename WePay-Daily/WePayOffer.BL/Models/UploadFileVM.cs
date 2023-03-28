using Microsoft.AspNetCore.Http;

namespace WePayOffer.BL.Models
{
    public class UploadFileVM
    {
        public IFormFile files { get; set; }
    }
}
