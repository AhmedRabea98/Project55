using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePayOffer.BL.Helper;
using WePayOffer.BL.Models;
using WePayOffer.BL.Repository;
using WePayOffer.DAL.Database;
using WePayOffer.DAL.Entity;

namespace WePayOffer.Portal.Controllers
{


    //[Authorize]
    public class BulkController : Controller
    {

        #region Fields

        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly FileUploader fileUploader;


        #endregion


        #region Ctor

        public BulkController(ApplicationContext context, IMapper mapper, IConfiguration configuration, FileUploader fileUploader)
        {
            this.unitOfWork = new UnitOfWork(context);
            this.mapper = mapper;
            this.configuration = configuration;
            this.fileUploader = fileUploader;
        }


        #endregion


        #region Actions

        public async Task<IActionResult> UploadBulk()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadBulk(UploadFileVM model)
        {
            try
            {

                var FilePathAfterUpload = await fileUploader.UploadFileAsync(model.files);

                var result = fileUploader.ReadDataFromExcel(FilePathAfterUpload);

                var data = mapper.Map<List<ServiceNumber>>(result);

                await unitOfWork.ServiceNumberRepository.CreateBulkAsync(data);

                await unitOfWork.SaveAsync();

                //var serviceNumber = await unitOfWork.ServiceNumberRepository.GetAsync(a => a.StatusId == 1);

                return RedirectToAction("UploadBulk");
            }

            catch (Exception ex)
            {
                using (var stream = new StreamWriter(configuration.GetSection("Keys").GetSection(@"ErrorPath").Value))
                {

                    stream.WriteLine(ex.Message);
                    stream.WriteLine(ex.InnerException);
                }
               // ViewBag.Error = "Failed To Upload";
                //return View();
            }
            return View();
        }


        #endregion
    }
}







