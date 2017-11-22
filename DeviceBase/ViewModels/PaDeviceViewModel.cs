using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceBase.Models;
using MetroSupport.ViewModels;

namespace DeviceBase.ViewModels
{
    public class PaDeviceViewModel
    {
        public PaDevice Device { get; set; }
        public LogViewModel Log {get;set;}
        public SelectList Locations { get; set; }
        public SelectList DeviceClass { get; set; }
        public SelectList DeviceType { get; set; }
        public SelectList DeviceSubType { get; set; }

        public PaDeviceViewModel()
        {
            Device = new PaDevice();
            Log = new LogViewModel();
        }
    }
}