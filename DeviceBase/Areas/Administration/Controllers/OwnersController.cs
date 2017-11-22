using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DeviceBase.Areas.Administration.ViewModels;
using DeviceBase.BLL.Services;
using DeviceBase.Models;
using MvcPaging;

namespace DeviceBase.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OwnersController : Controller
    {
       private readonly DeviceBaseService _deviceBaseService;
        
        public OwnersController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            OwnerViewModel owner = new OwnerViewModel();
            owner.Owners = _deviceBaseService.OwnerRepository.GetOwners().ToPagedList(currentpage,20);
            if (owner.Owners == null)
            {
                return HttpNotFound();
            }
            return View("Index", owner);
        }


        [HttpPost]
        public JsonResult GetOwnerInfo(string ownerid)
        {
            if (!String.IsNullOrWhiteSpace(ownerid))
            {
                Owner owner = _deviceBaseService.OwnerRepository.GetOwnerById(ownerid);
                if (owner != null)
                {
                    return Json(owner);
                }
            }
            return Json(null);
        }


        [HttpPost]
        public JsonResult CreateOwner(Owner owner)
        {
            if(ModelState.IsValid)
            {
                bool statement = owner.OwnerId == Guid.Empty
                    ? _deviceBaseService.OwnerRepository.CreateNewOwner(owner)
                    : _deviceBaseService.OwnerRepository.SaveOwner(owner);
                if (statement)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteOwners(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] ownerids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.OwnerRepository.DeleteOwners(ownerids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

    }
}
