using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.Models;
using MetroSupport.ViewModels;

namespace DeviceBase.Code.Interfaces
{
    public interface IDeviceRepository<T>
    {
        T GetDeviceById(string id);
        LogViewModel GetLogById(string id);
        IEnumerable<T> GetDevices();
        IEnumerable<T> GetDevicesByType(string devicetype);
        IEnumerable<T> GetDevicesByLocation(string location);
        IEnumerable<T> GetDevicesByInventory(string invnum);
        IEnumerable<T> GetDevicesByOwner(string owner);
        IEnumerable<T> GetDevicesBySerialNumber(string serialnumber);
        IEnumerable<T> Search(string searchitem);
        bool SaveDevice(T device, string username);
        bool DeleteDevice(string[] selectids);
    


        

    }
}