using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using WePayOffer.BL.Models;
using WePayOffer.BL.Repository;
using WePayOffer.DAL.Database;
using WePayOffer.DAL.Entity;

namespace WePayOffer.Portal.Controllers
{


   // [Authorize]
    public class WalletController : Controller
    {


        #region Fields

        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;


        #endregion

        #region Ctor

        public WalletController(ApplicationContext context, IMapper mapper)
        {
            this.unitOfWork = new UnitOfWork(context);
            this.mapper = mapper;
        }


        #endregion



        public IActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Filter(FilterVM filter)
        {


            try
            {
                var pageSize = int.Parse(Request.Form["length"].FirstOrDefault());
                var skip = int.Parse(Request.Form["start"].FirstOrDefault());

                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                IQueryable<ServiceTransaction> serviceTransaction = null;

                #region Filter

                var serviceTran = unitOfWork.ServiceNumberRepository.GetServiceTransaction();
                var serviceNum = unitOfWork.ServiceNumberRepository.GetServiceNumber();

                var query = (
                            from sn in serviceNum
                            join st in serviceTran on sn.Id equals st.ServiceNumberId into temp
                            from st in temp.DefaultIfEmpty()
                            select new
                            {
                                id = sn.Id,
                                serviceNumber = sn.Number,
                                offer = sn.OfferId,
                                uploadedDate = sn.CreationDate,
                                status = sn.StatusId,
                                responseMessage = st.ResponseMessage
                                //transactionDate = st.CreationDate.ToString()

                            }).ToList().Distinct().AsQueryable();

                if (filter.statusId != 0 && (filter.from.ToShortDateString() != "1/1/0001") && (filter.to.ToShortDateString() != "1/1/0001"))
                {
                    query = query.Where(m => m.status == filter.statusId && m.uploadedDate.Date >= filter.from.Date && m.uploadedDate.Date <= filter.to.Date);
                }
                else if (filter.statusId != 0)
                {
                    query = query.Where(m => m.status == filter.statusId);
                }
                else if ((filter.from.ToShortDateString() != "1/1/0001") && (filter.to.ToShortDateString() != "1/1/0001"))
                {
                    query = query.Where(m => m.uploadedDate.Date >= filter.from.Date && m.uploadedDate.Date <= filter.to.Date);
                }
                else
                {
                    query = query.Where(m =>
                    m.serviceNumber.Contains(searchValue) ||
                    m.offer.Contains(searchValue) ||
                    m.responseMessage.Contains(searchValue));
                }

                #endregion


                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    query = query.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));

                var data = query.Skip(skip).Take(pageSize).ToList();

                var recordsTotal = query.Count();

                var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };


                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
