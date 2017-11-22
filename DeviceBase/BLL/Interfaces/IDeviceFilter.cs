using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceBase.BLL.Interfaces
{
    public interface IDeviceFilter<T>
    {
        IQueryable<T> Filter(string deviceclass, string devicetype, string department, string location);
    }
}