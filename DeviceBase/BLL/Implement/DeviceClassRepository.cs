using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Castle.Components.DictionaryAdapter;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;

namespace DeviceBase.BLL.Implement
{
    public class DeviceClassRepository:IDeviceClassRepository
    {
        private DeviceContext _context;

        public DeviceClassRepository(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<DeviceClass> GetDeviceClasses()
        {
           return _context.DeviceClasses.OrderBy(x=>x.DeviceClassName);
        }

        public IQueryable<DeviceClass> GetDeviceClassesByDepartment(string department)
        {
            return _context.DeviceClasses.Where(x => x.Department == department);
        }

        public List<string> GetDeviceClassesGroups(string department)
        {
            
            var groupresults = department != "All" ?
                    _context.DeviceClasses.Where(x => x.Department == department)
                        .GroupBy(x => x.DeviceClassName)
                        .Select(g => new {Name = g.Key})
                     :
                    _context.DeviceClasses
                        .GroupBy(x => x.DeviceClassName)
                        .Select(g => new { Name = g.Key });

            List<string> resultvalues = new List<string>(); 
            foreach (var item in groupresults)
            {
                resultvalues.Add(item.Name);    
            }
            return resultvalues;
        }

       
        public DeviceClass GetDeviceClassById(string deviceclassid)
        {
            if (deviceclassid != String.Empty)
            {
                Guid guidid = Guid.Parse(deviceclassid);
                return _context.DeviceClasses.FirstOrDefault(x => x.Id == guidid);
            }
            return null;
        }

        public bool CreateNewDeviceClass(DeviceClass deviceclass)
        {
            
            Guid newidGuid = Guid.Empty;
            if (deviceclass.Id == newidGuid)
            {
                _context.DeviceClasses.Add(deviceclass);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveDeviceClass(DeviceClass deviceclass)
        {
            DeviceClass currentdeviceclass = _context.DeviceClasses.FirstOrDefault(x => x.Id == deviceclass.Id);
            if (currentdeviceclass != null)
            {
                currentdeviceclass.DeviceClassName = deviceclass.DeviceClassName;
               _context.Entry(currentdeviceclass).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public bool DeleteDeviceClasses(string[] deviceclassesid)
        {
            foreach (string id in deviceclassesid)
            {
                Guid deviceguid = Guid.Parse(id);
                DeviceClass deviceclass = _context.DeviceClasses.FirstOrDefault(x => x.Id == deviceguid);
                _context.Entry(deviceclass).State = EntityState.Deleted;
            }
            _context.SaveChanges();
            return true;

        }
    }
}