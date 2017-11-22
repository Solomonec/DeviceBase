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
    public class PaDeviceController : Controller
    {
        private readonly DeviceBaseService _deviceBaseService;
        public PaDeviceController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        public ActionResult Index(string deviceid = "")
        {
            PaDeviceViewModel device = new PaDeviceViewModel();
            if (String.IsNullOrWhiteSpace(deviceid))
            {
                device.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
                device.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("ПА"), "DeviceClassName", "DeviceClassName");
                return View(device);
            }
            device.Device = _deviceBaseService.PaDeviceRepository.GetDeviceById(deviceid);
            device.Log = _deviceBaseService.PaDeviceRepository.GetLogById(deviceid);
            device.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
            device.DeviceClass = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClassesByDepartment("ПА"), "DeviceClassName", "DeviceClassName");
            device.DeviceType = new SelectList(_deviceBaseService.DeviceTypeRepository.GetDeviceTypesByDeviceClass(device.Device.DeviceClass), "DeviceTypeName", "DeviceTypeName");
            device.DeviceSubType = new SelectList(_deviceBaseService.DeviceTypeRepository.GetDeviceSubTypesByDeviceType(device.Device.DeviceType), "DeviceSubTypeName", "DeviceSubTypeName");
          
            if (device.Device == null)
            {
                return HttpNotFound();
            }
            return View(device);

        }

        [HttpPost]
        public ActionResult Index(PaDevice device)
        {

            bool statement = false;
            if (ModelState.IsValid || ModelState.IsValidField("DevPaGenId") == false)
            {
                statement = _deviceBaseService.PaDeviceRepository.SaveDevice(device,User.Identity.Name);

            }
            if (statement)
            {
                PaDeviceViewModel deviceViewModel = new PaDeviceViewModel();
                deviceViewModel.Device = _deviceBaseService.PaDeviceRepository.GetDeviceById(device.DevPaGenId.ToString());
                return RedirectToAction("Index", new { deviceid = deviceViewModel.Device.DevPaGenId });
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
