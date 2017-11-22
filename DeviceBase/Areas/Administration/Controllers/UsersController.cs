using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DeviceBase.BLL.Services;
using DeviceBase.Extentions;
using DeviceBase.Models;
using DeviceBase.ViewModels;
using MvcPaging;
using WebMatrix.WebData;

namespace DeviceBase.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
         private readonly DeviceBaseService _deviceBaseService;
        public UsersController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }
    
        public ActionResult Index(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            IPagedList<UserProfile> users = _deviceBaseService.UserRepository.GetUsers().ToPagedList(currentpage, 20);
            
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        public ActionResult Manage(string username)
        {
         
            List<RoleViewModel> roleslist = new List<RoleViewModel>();
            string[] allroles = Roles.GetAllRoles();
            for (var i = 0; i < allroles.Count(); i++)
            {
                RoleViewModel role = new RoleViewModel();
                role.RoleName = allroles[i];
                if (Roles.IsUserInRole(username, role.RoleName))
                {
                    role.Selected = true;
                }
                roleslist.Add(role);
            }
            ManageModel profile = _deviceBaseService.UserRepository.GetUserByName(username).ToManageModel();
            profile.Roles = roleslist;

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageModel model)
        {
            if (Roles.GetRolesForUser(model.UserName).Any())
            {
                Roles.RemoveUserFromRoles(model.UserName, Roles.GetRolesForUser(model.UserName));
            }
            foreach (var role in model.Roles)
            {
                if (role.Selected)
                {
                    Roles.AddUserToRole(model.UserName, role.RoleName);
                }

            }

            List<RoleViewModel> roleslist = new List<RoleViewModel>();
            string[] allroles = Roles.GetAllRoles();
            for (var i = 0; i < allroles.Count(); i++)
            {
                RoleViewModel role = new RoleViewModel();
                role.RoleName = allroles[i];
                if (Roles.IsUserInRole(model.UserName, role.RoleName))
                {
                    role.Selected = true;
                }
                roleslist.Add(role);
            }

            ManageModel manage = _deviceBaseService.UserRepository.SaveUserProfile(model.ToUserProfile()).ToManageModel();
            manage.Roles = roleslist;

            return View(manage);
        }



       public ActionResult Registration()
        {
            RegisterModel model = new RegisterModel();
            List<RoleViewModel> roleslist = new List<RoleViewModel>();
            string[] allroles = Roles.GetAllRoles();
            for (var i = 0; i < allroles.Count(); i++)
            {
                RoleViewModel role = new RoleViewModel
                {
                    RoleName = allroles[i],
                    Selected = false
                };
                roleslist.Add(role);
            }
            model.Roles = roleslist;
 
            return View(model);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                  if (!WebSecurity.UserExists(model.UserName))
                  {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { FullName = model.FullName, Job = model.Job, Slugba = model.Slugba, Department = model.Department, Tel = model.Tel, Active = true });

                    
                        foreach (var role in model.Roles)
                        {
                            if (role.Selected)
                            {
                                Roles.AddUserToRole(model.UserName, role.RoleName);
                            }

                        }
                  }

                    return RedirectToAction("Index", "Users");

                }
                catch (MembershipCreateUserException e)
                {
                   // ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
          
            
            return View(model);
        }

        public ActionResult DeleteUser(string userName)
        {


            if (Roles.GetRolesForUser(userName).Any())
            {
                Roles.RemoveUserFromRoles(userName, Roles.GetRolesForUser(userName));
            }
            ((SimpleMembershipProvider) Membership.Provider).DeleteAccount(userName);
            ((SimpleMembershipProvider) Membership.Provider).DeleteUser(userName, true);
            
            return RedirectToAction("Index", "Users");
        }

      
        [HttpPost]
        public JsonResult ChangePassword(string username, string newpassword)
        {
            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(newpassword))
            {
                bool changePasswordSucceeded;
                try
                {
                    string resettoken = WebSecurity.GeneratePasswordResetToken(username);
                    changePasswordSucceeded = WebSecurity.ResetPassword(resettoken, newpassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return Json(true);
                }
                return Json(false);
            }
            return Json(false);
        }

        [ChildActionOnly]
        public ActionResult UserProfile()
        {
            UserProfile userProfile = _deviceBaseService.UserRepository.GetUserByName(User.Identity.Name);

            return PartialView("_LoginPartial", userProfile);
        }
       

      
        
    }
}
