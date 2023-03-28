using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using WePayOffer.BL.Models;
using WePayOffer.BL.Repository;
using WePayOffer.DAL.Database;
using WePayOffer.DAL.Extend;

namespace WePayOffer.Portal.Controllers
{

    //[Authorize]
    public class UsersController : Controller
    {

        #region Fields

        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;


        #endregion

        #region Ctor

        public UsersController(ApplicationContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = new UnitOfWork(context);
            this.mapper = mapper;
            this.userManager = userManager;
        }


        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            user.IsDeleted = true;
            var result = await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        #endregion

        #region Ajax Call

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

                IQueryable<ApplicationUser> user;

                #region Filter


                if ((filter.from.ToShortDateString() != "1/1/0001") && (filter.to.ToShortDateString() != "1/1/0001"))
                {
                    user = userManager.Users.Include(a => a.Users).Where(m => m.CreatedOn.Date >= filter.from.Date && m.CreatedOn.Date <= filter.to.Date && m.IsDeleted == false);
                }
                else
                {
                    if (searchValue != null)
                    {
                        user = userManager.Users.Include(a => a.Users).Where(m => m.UserName.Contains(searchValue) || m.FullName.Contains(searchValue)).Where(m => m.IsDeleted == false);
                    }
                    else
                    {
                        user = userManager.Users.Include(a => a.Users).Where(a => a.IsDeleted == false);
                    }
                }

                #endregion


                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    user = user.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));

                var result = mapper.Map<IEnumerable<ApplicationUserVM>>(user);

                var data = result.Skip(skip).Take(pageSize).ToList().OrderByDescending(a => a.CreatedOn);

                var recordsTotal = result.Count();

                var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };

                return Ok(jsonData);


            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        #endregion
    }
}
