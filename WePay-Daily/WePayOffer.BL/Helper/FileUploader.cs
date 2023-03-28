using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using WePayOffer.BL.Models;

namespace WePayOffer.BL.Helper
{
    public class FileUploader
    {


        private readonly IConfiguration configuration;

        public FileUploader(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> UploadFileAsync(IFormFile File)
        {

            try
            {
                // 1 ) Get Directory
                string FolderPath = Directory.GetCurrentDirectory() + configuration.GetSection("Keys").GetSection(@"FolderPath").Value;


                //2) Get File Name
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);


                // 3) Merge Path with File Name
                string FinalPath = Path.Combine(FolderPath, FileName);


                //4) Save File As Streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    await File.CopyToAsync(Stream);
                }

                return FinalPath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public IEnumerable<ServiceNumberVM> ReadDataFromExcel(string FilePathAfterUpload)
        {
            #region Read Excel File

            var Bulk = new List<ServiceNumberVM>();

            //Upload and save the file

            string csvPath = FilePathAfterUpload;


            //Create a DataTable.

            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[2] {

                new DataColumn(configuration.GetSection("Keys").GetSection("Number").Value, typeof(string)),
                new DataColumn(configuration.GetSection("Keys").GetSection("OfferId").Value, typeof(string))

                });



            //Read the contents of CSV file.

            string csvData = System.IO.File.ReadAllText(csvPath);



            //Execute a loop over the rows.

            foreach (string row in csvData.Split('\n'))

            {

                if (!string.IsNullOrEmpty(row))

                {

                    dt.Rows.Add();

                    int i = 0;



                    //Execute a loop over the columns.

                    foreach (string cell in row.Split(','))

                    {

                        dt.Rows[dt.Rows.Count - 1][i] = cell;

                        i++;

                    }

                }

            }

            foreach (DataRow item in dt.Rows)
            {

                var serviceNumberVM = new ServiceNumberVM()
                {
                    Number = item["Number"].ToString(),
                    OfferId = item["OfferId"].ToString(),
                    StatusId = 1
                };

                Bulk.Add(serviceNumberVM);
            }

            Bulk.RemoveAt(0);

            #endregion

            return Bulk;
        }

    }
}
