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
    public class DeviceClassesController : Controller
    {
        
         private readonly DeviceBaseService _deviceBaseService;
        
        public DeviceClassesController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            DeviceClassViewModel deviceclass = new DeviceClassViewModel();
            deviceclass.DeviceClasses = _deviceBaseService.DeviceClassRepository.GetDeviceClasses().ToPagedList(currentpage,20);
            if (deviceclass.DeviceClasses == null)
            {
                return HttpNotFound();
            }
            return View("Index", deviceclass);
        }

        [HttpPost]
        public JsonResult CreateDeviceClass(DeviceClass deviceclass)
        {
            if(ModelState.IsValid)
            {
                bool statement = deviceclass.Id == Guid.Empty
                    ? _deviceBaseService.DeviceClassRepository.CreateNewDeviceClass(deviceclass)
                    : _deviceBaseService.DeviceClassRepository.SaveDeviceClass(deviceclass);
                if (statement)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteDeviceClasses(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] deviceclassids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.DeviceClassRepository.DeleteDeviceClasses(deviceclassids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

    }
}
