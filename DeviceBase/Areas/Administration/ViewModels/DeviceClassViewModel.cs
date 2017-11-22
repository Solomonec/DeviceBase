using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Implement;
using DeviceBase.Models;
using MvcPaging;

namespace DeviceBase.Areas.Administration.ViewModels
{
    public class DeviceClassViewModel
    {
        public IPagedList<DeviceClass> DeviceClasses { get; set; }

        public DeviceClass DeviceClass { get; set; }

        public DeviceClassViewModel()
        {
            DeviceClass = new DeviceClass();
        }


    }
}