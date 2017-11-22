using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DeviceBase.BLL.Implement.Export;
using DeviceBase.BLL.Services;
using DeviceBase.Code.Implement;
using DeviceBase.Models;
using DeviceBase.Code.Interfaces;
using DeviceBase.Extentions;
using DeviceBase.ViewModels;
using MvcPaging;

namespace DeviceBase.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly DeviceBaseService _deviceBaseService;
        
        public HomeController(DeviceBaseService deviceBaseService)
        {
            _deviceBaseService = deviceBaseService;
        }

        [HttpGet]
        public ActionResult ItDevices(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            IPagedList<ItDevice> devices = _deviceBaseService.ItDeviceRepository.GetDevices().ToPagedList(currentpage,40);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View("ItDevices", devices);
        }

        [HttpGet]
        public ActionResult ItDevicesByType(int? page, string devicetype)
        {
            if (String.IsNullOrWhiteSpace(devicetype))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.devicetype = devicetype;
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            var devices = _deviceBaseService.ItDeviceRepository.GetDevicesByType(devicetype).ToPagedList(currentpage,40);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View("ItDevices",devices);

        }

        [HttpGet]
        public ActionResult AsppDevices(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            IPagedList<AsppDevice> devices = _deviceBaseService.AsppDeviceRepository.GetDevices().ToPagedList(currentpage, 40);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View("AsppDevices", devices);
        }

        [HttpGet]
        public ActionResult AsppDevicesByType(int? page, string devicetype)
        {
            if (String.IsNullOrWhiteSpace(devicetype))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.devicetype = devicetype;
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            var devices = _deviceBaseService.AsppDeviceRepository.GetDevicesByType(devicetype).ToPagedList(currentpage, 40);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View("AsppDevices", devices);

        }

        [HttpGet]
        public ActionResult PaDevices(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            IPagedList<PaDevice> devices = _deviceBaseService.PaDeviceRepository.GetDevices().ToPagedList(currentpage, 40);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View("PaDevices", devices);
        }

        [HttpGet]
        public ActionResult PaDevicesByType(int? page, string devicetype)
        {
            if (String.IsNullOrWhiteSpace(devicetype))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.devicetype = devicetype;
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            var devices = _deviceBaseService.PaDeviceRepository.GetDevicesByType(devicetype).ToPagedList(currentpage, 40);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View("PaDevices", devices);

        }

        [HttpPost]
        public JsonResult DeleteItDevices(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] deviceids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.ItDeviceRepository.DeleteDevice(deviceids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

        [HttpPost]
        public JsonResult DeletePaDevices(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] deviceids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.PaDeviceRepository.DeleteDevice(deviceids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

        [HttpPost]
        public JsonResult DeleteAsppDevices(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] deviceids = Regex.Split(selectedIds, ";");
            bool statement = _deviceBaseService.AsppDeviceRepository.DeleteDevice(deviceids);
            if (statement != true)
            {
                return Json(false);
            }
            return Json(true);

        }

        [HttpGet]
        public ActionResult SearchItDevices(int? page,string searchvalue)
        {

            ViewBag.searchvalue = searchvalue;
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            var searchresults = _deviceBaseService.ItDeviceRepository.Search(searchvalue).ToPagedList(currentpage, 40);
            if (searchvalue == null)
            {
                return HttpNotFound();
            }
            return View("ItDevices", searchresults);

        }

        public ActionResult SearchAsppDevices(int? page, string searchvalue)
        {

            ViewBag.searchvalue = searchvalue;
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            var searchresults = _deviceBaseService.AsppDeviceRepository.Search(searchvalue).ToPagedList(currentpage, 40);
            if (searchvalue == null)
            {
                return HttpNotFound();
            }
            return View("AsppDevices", searchresults);

        }

        public ActionResult SearchPaDevices(int? page, string searchvalue)
        {

            ViewBag.searchvalue = searchvalue;
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            var searchresults = _deviceBaseService.PaDeviceRepository.Search(searchvalue).ToPagedList(currentpage, 40);
            if (searchvalue == null)
            {
                return HttpNotFound();
            }
            return View("PaDevices", searchresults);

        }


        public FileResult Export(string devicetype, string deviceclass)
        {
            List<FilterResultModel> resultdevices =_deviceBaseService.DataFilter.Filter(Convertion.GetEntry(deviceclass), deviceclass, devicetype, String.Empty, String.Empty).ToList();
            
            if (resultdevices.Count > 0)
            {
              byte[] filedata = _deviceBaseService.DataExport.ExportTo(new MsExcel(), resultdevices.ListToDataTable());
              return File(filedata, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Devices.xlsx");
        
            }
            return File("", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Devices.xlsx");
        }

    }
}
