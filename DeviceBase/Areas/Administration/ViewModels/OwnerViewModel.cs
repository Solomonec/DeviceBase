using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Implement;
using DeviceBase.Models;
using MvcPaging;

namespace DeviceBase.Areas.Administration.ViewModels
{
    public class OwnerViewModel
    {
        public IPagedList<Owner> Owners { get; set; }

        public Owner Owner { get; set; }

        public OwnerViewModel()
        {
            Owner = new Owner();
        }


    }
}