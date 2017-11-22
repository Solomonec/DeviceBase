using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DeviceBase.BLL.Services;
using DeviceBase.Code.Implement;
using DeviceBase.Models;
using DeviceBase.ViewModels;

namespace DeviceBase.Controllers
{
    public class AsppDeviceController : Controller
    {
        private readonly DeviceBaseService _deviceBaseService;
        public AsppDeviceController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        public ActionResult Index(string deviceid = "")
        {
            AsppDeviceViewModel device = new AsppDeviceViewModel();
            if (String.IsNullOrWhiteSpace(deviceid))
            {
                device.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
                device.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("АСПП"), "DeviceClassName", "DeviceClassName");
                return View(device);
            }
            device.Device = _deviceBaseService.AsppDeviceRepository.GetDeviceById(deviceid);
            device.Log = _deviceBaseService.AsppDeviceRepository.GetLogById(deviceid);
            device.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
            device.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("АСПП"), "DeviceClassName", "DeviceClassName");
            device.DeviceType = new SelectList(_deviceBaseService.DeviceTypeRepository.GetDeviceTypesByDeviceClass(device.Device.DeviceClass), "DeviceTypeName", "DeviceTypeName");

            if (device.Device == null)
            {
                return HttpNotFound();
            }
            return View(device);

        }

        [HttpPost]
        public ActionResult Index(AsppDevice device)
        {

            bool statement = false;
            if (ModelState.IsValid || ModelState.IsValidField("DevAsppGenId") == false)
            {
                statement = _deviceBaseService.AsppDeviceRepository.SaveDevice(device, User.Identity.Name);

            }
            if (statement)
            {
                AsppDeviceViewModel deviceViewModel = new AsppDeviceViewModel();
                deviceViewModel.Device = _deviceBaseService.AsppDeviceRepository.GetDeviceById(device.DevAsppGenId.ToString());

                return RedirectToAction("Index", new { deviceid = deviceViewModel.Device.DevAsppGenId });
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
