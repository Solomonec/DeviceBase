using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Implement;
using DeviceBase.Models;
using MvcPaging;

namespace DeviceBase.Areas.Administration.ViewModels
{
    public class LocationViewModel
    {
        public IPagedList<Location> Locations { get; set; }

        public Location Location { get; set; }

        public LocationViewModel()
        {
            Location = new Location();
        }


    }
}