using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.Models;

namespace DeviceBase.BLL.Interfaces
{
    public interface IDeviceTypeRepository
    {
        IQueryable<DeviceType> GetDeviceTypes();
        DeviceType GetDeviceTypeById(string devicetypeid);
        List<string> GetDeviceTypesByClassName(string classname);
        List<string> GetDeviceSubTypesByType(string typename);
        IQueryable<DeviceType> GetDeviceSubTypesByDeviceType(string typename);
        IQueryable<DeviceType> GetDeviceTypesByDeviceClass(string classname);
        bool CreateNewDeviceType(DeviceType devicetype);
        bool SaveDeviceType(DeviceType devicetype);
        bool DeleteDeviceTypes(string[] devicetypesid);
    }
}
