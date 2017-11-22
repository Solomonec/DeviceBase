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
    public class PaDeviceFilter:IDeviceFilter<PaDevice>
    {
        private readonly DeviceContext _context;

        public PaDeviceFilter(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<PaDevice> Filter(string deviceclass, string devicetype, string department, string location)
        {

            IQueryable<PaDevice> devices = _context.PaDevices.ToList().AsQueryable();
            DeviceFilter<PaDevice> devicefilter = new MetroDeviceFilter<PaDevice>(devices);
            if(!String.IsNullOrWhiteSpace(deviceclass))
                devicefilter = new SetDeviceFilterOption<PaDevice>(x => x.DeviceClass.Contains(deviceclass), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(devicetype))
                devicefilter = new SetDeviceFilterOption<PaDevice>(x => x.DeviceType.Contains(devicetype), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(department))
                devicefilter = new SetDeviceFilterOption<PaDevice>(x => x.DeviceOwner.Slugba.Contains(department), devices, devicefilter);
            if (!String.IsNullOrWhiteSpace(location))
                devicefilter = new SetDeviceFilterOption<PaDevice>(x => x.Location.Contains(location), devices, devicefilter);
            IQueryable<PaDevice> resultdevices = devicefilter.FilterResult();

           return resultdevices;
        
        }
    }
}