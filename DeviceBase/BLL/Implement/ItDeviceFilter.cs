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
    public class ItDeviceFilter:IDeviceFilter<ItDevice>
    {
        private readonly DeviceContext _context;

        public ItDeviceFilter(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<ItDevice> Filter(string deviceclass, string devicetype, string department, string location)
        {

            IQueryable<ItDevice> devices = _context.ItDevices.ToList().AsQueryable();
            DeviceFilter<ItDevice> devicefilter = new MetroDeviceFilter<ItDevice>(devices);
            if(!String.IsNullOrWhiteSpace(deviceclass))
                devicefilter = new SetDeviceFilterOption<ItDevice>(x => x.DeviceClass.Contains(deviceclass), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(devicetype))
                devicefilter = new SetDeviceFilterOption<ItDevice>(x => x.DeviceType.Contains(devicetype), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(department))
                devicefilter = new SetDeviceFilterOption<ItDevice>(x => x.DeviceOwner.Slugba.Contains(department), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(location))
                devicefilter = new SetDeviceFilterOption<ItDevice>(x => x.Location.Contains(location), devices, devicefilter);
            IQueryable<ItDevice> resultdevices = devicefilter.FilterResult();

           return resultdevices;
        
        }
    }
}