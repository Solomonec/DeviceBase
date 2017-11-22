using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;
using Microsoft.Ajax.Utilities;

namespace DeviceBase.BLL.Implement
{
    public class DeviceTypeRepository:IDeviceTypeRepository
    {
        private readonly DeviceContext _context;
        public DeviceTypeRepository(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<DeviceType> GetDeviceTypes()
        {
            return _context.DeviceTypes.OrderBy(x=>x.DeviceTypeName);
        }

        public DeviceType GetDeviceTypeById(string devicetypeid)
        {
            if (devicetypeid != String.Empty)
            {
                Guid guidid = Guid.Parse(devicetypeid);
                return _context.DeviceTypes.FirstOrDefault(x => x.Id == guidid);
            }
            return null;
        }

        public List<string> GetDeviceTypesByClassName(string classname)
        {
            if (classname != String.Empty)
            {
                IEnumerable<DeviceType> devicetypes  = _context.DeviceTypes.Where(x => x.DeviceClassName == classname).DistinctBy(x=>x.DeviceTypeName);
                List<string> resultvalues = new List<string>();
                    foreach (var item in devicetypes)
                    {
                        resultvalues.Add(item.DeviceTypeName);
                    }
                
                return resultvalues;
            }
            return null;
        }

        public List<string> GetDeviceSubTypesByType(string typename)
        {
            if (typename != String.Empty)
            {
                IQueryable<DeviceType> devicetypes = _context.DeviceTypes.Where(x => x.DeviceTypeName == typename);
                List<string> resultvalues = new List<string>();
                foreach (var item in devicetypes)
                {
                    resultvalues.Add(item.DeviceSubTypeName);
                }

                return resultvalues;
            }
            return null;
        }

        public IQueryable<DeviceType> GetDeviceSubTypesByDeviceType(string typename)
        {
            if (typename != String.Empty)
            {
                return _context.DeviceTypes.Where(x => x.DeviceTypeName == typename);

            }
            return null;
        }

        public IQueryable<DeviceType> GetDeviceTypesByDeviceClass(string classname)
        {
            if (classname != String.Empty)
            {
                return  _context.DeviceTypes.Where(x => x.DeviceClassName == classname);
              
            }
            return null;
        }

        public bool CreateNewDeviceType(DeviceType devicetype)
        {
            Guid newidGuid = Guid.Empty;
            if (devicetype.Id == newidGuid)
            {
                _context.DeviceTypes.Add(devicetype);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveDeviceType(DeviceType devicetype)
        {
            DeviceType currentdevicetype = _context.DeviceTypes.FirstOrDefault(x => x.Id == devicetype.Id);
            if (currentdevicetype != null)
            {
                currentdevicetype.DeviceClassName = devicetype.DeviceClassName;
                currentdevicetype.DeviceTypeName = devicetype.DeviceTypeName;
                
                _context.Entry(currentdevicetype).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteDeviceTypes(string[] devicetypesid)
        {
            if (devicetypesid != null)
            {
                foreach (string id in devicetypesid)
                {
                    Guid guidid = Guid.Parse(id);
                    DeviceType devicetype = _context.DeviceTypes.FirstOrDefault(x => x.Id == guidid);
                    _context.Entry(devicetype).State = EntityState.Deleted;
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}