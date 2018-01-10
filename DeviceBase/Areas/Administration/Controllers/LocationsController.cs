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

    public class LocationsController : Controller
    {
         private readonly DeviceBaseService _deviceBaseService;

         public LocationsController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            LocationViewModel location = new LocationViewModel();
            location.Locations = _deviceBaseService.LocationRepository.GetLocations().ToPagedList(currentpage,20);
            if (location.Locations == null)
            {
                return HttpNotFound();
            }
            return View("Index", location);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult CreateLocation(Location location)
        {
            if(ModelState.IsValid)
            {
                bool statement = location.LocationId== Guid.Empty
                    ? _deviceBaseService.LocationRepository.CreateNewLocation(location)
                    : _deviceBaseService.LocationRepository.SaveLocation(location);
                if (statement)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteLocations(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] locationids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.LocationRepository.DeleteLocations(locationids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

        [HttpPost]
        [Authorize]
        public JsonResult GetLocations()
        {
            IQueryable<Location> locations = _deviceBaseService.LocationRepository.GetLocations();
            if (locations == null)
            {
                return Json(null);
            }
            return Json(locations);

        }

    }
}
