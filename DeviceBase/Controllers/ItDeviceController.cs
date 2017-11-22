using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DeviceBase.BLL.Services;
using DeviceBase.Code.Implement;
using DeviceBase.Extentions;
using DeviceBase.Models;
using DeviceBase.ViewModels;

namespace DeviceBase.Controllers
{
    public class ItDeviceController : Controller
    {
        private readonly DeviceBaseService _deviceBaseService;
        public ItDeviceController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        public ActionResult Index(string deviceid = "")
        {
            ItDeviceViewModel device = new ItDeviceViewModel();
            if (String.IsNullOrWhiteSpace(deviceid))
            {
                device.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
                device.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("ИТ"), "DeviceClassName", "DeviceClassName");
                return View(device);
            }
            device.Device = _deviceBaseService.ItDeviceRepository.GetDeviceById(deviceid);
            device.Log = _deviceBaseService.ItDeviceRepository.GetLogById(deviceid);
            device.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
            device.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("ИТ"), "DeviceClassName", "DeviceClassName");
            device.DeviceType = new SelectList(_deviceBaseService.DeviceTypeRepository.GetDeviceTypesByDeviceClass(device.Device.DeviceClass), "DeviceTypeName", "DeviceTypeName");
              

            if (device.Device == null)
            {
                return HttpNotFound();
            }
            return View(device);

        }

        [HttpPost]
        public ActionResult Index(ItDevice device)
        {
            bool statement = false;
            if (ModelState.IsValid | ModelState.IsValidField("DevItGenId") == false)
            {
              statement = _deviceBaseService.ItDeviceRepository.SaveDevice(device,User.Identity.Name);
                
            }
            if (statement)
            {
                ItDeviceViewModel deviceViewModel = new ItDeviceViewModel();
                deviceViewModel.Device = _deviceBaseService.ItDeviceRepository.GetDeviceById(device.DevItGenId.ToString());
                //deviceViewModel.Log = _deviceBaseService.ItDeviceRepository.GetLogById(device.DevItGenId.ToString());
                //deviceViewModel.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
                //deviceViewModel.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("ИТ"), "DeviceClassName", "DeviceClassName");
                //deviceViewModel.DeviceType = new SelectList(_deviceBaseService.DeviceTypeRepository.GetDeviceTypesByDeviceClass(deviceViewModel.Device.DeviceClass), "DeviceTypeName", "DeviceTypeName");
                return RedirectToAction("Index",new{deviceid=deviceViewModel.Device.DevItGenId});
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public JsonResult GetOwnersList(string searchval)
        {
            if (String.IsNullOrWhiteSpace(searchval))
            {
                return Json(null);
            }
            IQueryable<Owner> owners = _deviceBaseService.OwnerRepository.GetOwnersByName(searchval);
            if (owners == null)
            {
                return Json(null);
            }
            return Json(owners);
        }

    }
}
