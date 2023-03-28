using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WePayOffer.BL.Models;
using WePayOffer.DAL.Extend;

namespace WePayOffer.Portal.Controllers
{

    public class AccountController : Controller
    {

        #region Fields

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        #endregion


        #region Ctor

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #endregion


        #region Actions

        [HttpGet]
        //[Authorize]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserVM model)
        {

            try
            {

                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.UserName + "@.te.eg",
                    FullName = model.FullName,
                    CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CreatedOn = model.CreatedOn,
                    IsDeleted = model.IsDeleted

                };


                var result = await userManager.CreateAsync(user, model.UserName);

                if (result.Succeeded)
                {
                    ModelState.Clear();
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }

                return View(model);

            }
            catch (Exception ex)
            {
                return View(model);
            }

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {

            try
            {
                #region Check Active Directory

                //using (var ldapConnection = new LdapConnection())
                //{
                //    //ldapConnection.Connect("10.11.240.22", 3268);
                //    //ldapConnection.Bind(@"Egypt\mahmoud.s.mohamad", "Comeflywithme@310190");

                //    ldapConnection.Connect("10.11.240.22", 3268);
                //    ldapConnection.Bind(@"Egypt\" + model.UserName, model.Password);
                //}


               // return RedirectToAction("Index", "Home");

                #endregion

                #region Check Local Database

                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null && user.IsDeleted == false)
                {

                    var result = await signInManager.PasswordSignInAsync(user, model.UserName.ToLower(), model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }


                ModelState.AddModelError("", "user not in local database");
                return View(model);

                #endregion
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "userName/password not match active directory");
                return View(model);
            }

        }


        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

    }
}
