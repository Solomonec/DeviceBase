using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceBase.Extentions;
using DeviceBase.ViewModels;
using DeviceBase.BLL.Implement;
using DeviceBase.Models;
using DeviceBase.BLL.Implement.Export;
using DeviceBase.BLL.Services;

namespace DeviceBase.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly DeviceBaseService _deviceBaseService;
        public StatisticsController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        public ActionResult Devices()
        {
            DeviceStatisticsViewModel devicestatisticviewmodel = new DeviceStatisticsViewModel();
            devicestatisticviewmodel.DeviceClasses = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClasses(), "DeviceClassName", "DeviceClassName");
            devicestatisticviewmodel.Departments = new SelectList(_deviceBaseService.OwnerRepository.GetOwnersDepartments());
            devicestatisticviewmodel.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
            return View(devicestatisticviewmodel);
        }

        [HttpPost]
        public ActionResult Devices(DeviceStatisticsViewModel devicestatistics)
        {
            List<FilterResultModel> resultdevices = _deviceBaseService.DataFilter.Filter(Convertion.GetEntry(devicestatistics.CurrentDeviceClass), devicestatistics.CurrentDeviceClass, String.Empty, devicestatistics.CurrentDepartment, devicestatistics.CurrentLocation).ToList();
            devicestatistics.Statistics = _deviceBaseService.Statistics.GetDevicesStatistics(resultdevices.AsEnumerable());
            devicestatistics.DeviceClasses = new SelectList(_deviceBaseService.DeviceClassRepository.GetDeviceClasses(), "DeviceClassName", "DeviceClassName");
            devicestatistics.Departments = devicestatistics.CurrentDepartment != String.Empty ? new SelectList(_deviceBaseService.OwnerRepository.GetOwnersDepartments(), devicestatistics.CurrentDepartment) : new SelectList(_deviceBaseService.OwnerRepository.GetOwnersDepartments()); 
            devicestatistics.Locations = new SelectList(_deviceBaseService.LocationRepository.GetLocations(), "LocationName", "LocationName");
            return View(devicestatistics);
        }
    }
}
