using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.Models;

namespace DeviceBase.BLL.Interfaces
{
    public interface IDeviceClassRepository
    {
        IQueryable<DeviceClass> GetDeviceClasses();
        List<string> GetDeviceClassesGroups(string department);
        IQueryable<DeviceClass> GetDeviceClassesByDepartment(string department);
        DeviceClass GetDeviceClassById(string deviceclassid);
        bool CreateNewDeviceClass(DeviceClass deviceclass);
        bool SaveDeviceClass(DeviceClass deviceclass);
        bool DeleteDeviceClasses(string[] deviceclassesid);
    }
}
