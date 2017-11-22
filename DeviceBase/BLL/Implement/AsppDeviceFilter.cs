using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Implement.Filter;
using DeviceBase.BLL.Interfaces;
using DeviceBase.BLL.Interfaces.Filter;
using DeviceBase.Models;

namespace DeviceBase.BLL
{
    public class AsppDeviceFilter:IDeviceFilter<AsppDevice>
    {
        private readonly DeviceContext _context;

        public AsppDeviceFilter(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<AsppDevice> Filter(string deviceclass, string devicetype, string department, string location)
        {

            IQueryable<AsppDevice> devices = _context.AsppDevices.ToList().AsQueryable();
            DeviceFilter<AsppDevice> devicefilter = new MetroDeviceFilter<AsppDevice>(devices);
            if(!String.IsNullOrWhiteSpace(deviceclass))
                devicefilter = new SetDeviceFilterOption<AsppDevice>(x => x.DeviceClass.Contains(deviceclass), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(devicetype))
                devicefilter = new SetDeviceFilterOption<AsppDevice>(x => x.DeviceType.Contains(devicetype), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(department))
                devicefilter = new SetDeviceFilterOption<AsppDevice>(x => x.DeviceOwner.Slugba.Contains(department), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(location))
                devicefilter = new SetDeviceFilterOption<AsppDevice>(x => x.Location.Contains(location), devices, devicefilter);
            IQueryable<AsppDevice> resultdevices = devicefilter.FilterResult();

           return resultdevices;
        
        }
    }
}