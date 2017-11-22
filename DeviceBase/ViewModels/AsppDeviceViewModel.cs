using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceBase.Models;
using MetroSupport.ViewModels;

namespace DeviceBase.ViewModels
{
    public class AsppDeviceViewModel
    {
        public AsppDevice Device { get; set; }
        public LogViewModel Log {get;set;}
        public SelectList Locations { get; set; }
        public SelectList DeviceClass { get; set; }
        public SelectList DeviceType { get; set; }
        public AsppDeviceViewModel()
        {
            Device = new AsppDevice();
            Log = new LogViewModel();
        }
    }
}