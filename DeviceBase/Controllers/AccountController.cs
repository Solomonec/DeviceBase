using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DeviceBase.BLL.Services;
using DeviceBase.Extentions;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using DeviceBase.Models;

namespace DeviceBase.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DeviceBaseService _deviceBaseService;

        public AccountController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            UserProfile profile = _deviceBaseService.UserRepository.GetUserByName(model.UserName);
            if (profile != null)
            {
                if (profile.Active)
                {
                    if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                    {
                        return RedirectToAction("ItDevices", "Home");
                    }
                    ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
                }
                return PartialView("Error");
            }
            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Account");
        }

        [ChildActionOnly]
        public ActionResult UserProfile()
        {
            UserProfile userProfile = _deviceBaseService.UserRepository.GetUserByName(User.Identity.Name);
           
            return PartialView("_LoginPartial", userProfile);
        }

    }
}
