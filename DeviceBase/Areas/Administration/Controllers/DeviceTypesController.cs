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
    
    public class DeviceTypesController : Controller
    {
        private readonly DeviceBaseService _deviceBaseService;
        
        public DeviceTypesController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            DeviceTypeViewModel devicetype = new DeviceTypeViewModel();
            devicetype.DeviceTypes = _deviceBaseService.DeviceTypeRepository.GetDeviceTypes().ToPagedList(currentpage,20);
            if (devicetype.DeviceTypes == null)
            {
                return HttpNotFound();
            }
            return View("Index", devicetype);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult GetDeviceTypeInfo(string devicetypeid)
        {
            if (!String.IsNullOrWhiteSpace(devicetypeid))
            {
                DeviceType devicetype = _deviceBaseService.DeviceTypeRepository.GetDeviceTypeById(devicetypeid);
                if (devicetype != null)
                {
                    return Json(devicetype);
                }
            }
            return Json(null);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult CreateDeviceTypes(DeviceType devicetype)
        {
            if(ModelState.IsValid)
            {
                bool statement = devicetype.Id == Guid.Empty
                    ? _deviceBaseService.DeviceTypeRepository.CreateNewDeviceType(devicetype)
                    : _deviceBaseService.DeviceTypeRepository.SaveDeviceType(devicetype);
                if (statement)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }


        [HttpPost]
        [Authorize]
        public JsonResult GetDeviceTypesClasses()
        {
                List<string> deviceclasses = _deviceBaseService.DeviceClassRepository.GetDeviceClassesGroups("All");
                if (deviceclasses != null)
                {
                    return Json(deviceclasses);
                }
            return Json(null);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetDeviceSubTypesByType(string typename)
        {
            List<string> devicesubtypes = _deviceBaseService.DeviceTypeRepository.GetDeviceSubTypesByType(typename);
            if (devicesubtypes != null)
                return Json(devicesubtypes);
            
            return Json(null);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetDeviceTypesByClass(string classname)
        {
            List<string> devicetypes = _deviceBaseService.DeviceTypeRepository.GetDeviceTypesByClassName(classname);
           
            if(devicetypes != null)
                return Json(devicetypes);
            
            return Json(null);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteDeviceTypes(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] devicetypeids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.DeviceTypeRepository.DeleteDeviceTypes(devicetypeids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

    }
}
