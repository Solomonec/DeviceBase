using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Implement;
using DeviceBase.Models;
using MvcPaging;

namespace DeviceBase.Areas.Administration.ViewModels
{
    public class DeviceTypeViewModel
    {
        public IPagedList<DeviceType> DeviceTypes { get; set; }

        public DeviceType DeviceType { get; set; }

        public DeviceTypeViewModel()
        {
            DeviceType = new DeviceType();
        }


    }
}