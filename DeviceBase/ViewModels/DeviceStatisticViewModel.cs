using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceBase.ViewModels
{
    public class DeviceStatisticsViewModel
    {
        public string CurrentDeviceClass { get; set; }
        public string CurrentDepartment { get; set; }
        public string CurrentLocation { get; set; }
        public SelectList DeviceClasses { get; set; }
        public SelectList Departments { get; set; }
        public SelectList Locations { get; set; }

        public IEnumerable<StatisticsResultModel> Statistics { get; set; } 
    }
}