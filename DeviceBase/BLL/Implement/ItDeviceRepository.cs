using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;
using MetroSupport.ViewModels;
using Microsoft.Ajax.Utilities;
using Ninject;

namespace DeviceBase.Code.Implement
{
    public class ItDeviceRepository : IDeviceRepository<ItDevice>

    {
        private readonly DeviceContext _context;

        private ILogRepository<ItDeviceLog> Itlog { get; set; }
        
        public ItDeviceRepository(DeviceContext context)
        {
           _context = context;
            Itlog = new ItLogRepository(context);
          
        }

        public LogViewModel GetLogById(string id)
        {
            ItDeviceLog itlog = Itlog.GetLogById(id);
            LogViewModel log = new LogViewModel
            {
                CreateDate = itlog.CreationDate,
                ChangeDate = itlog.LastUpdateDate,
                WhoChange = itlog.WhoLastUpdate,
                WhoCreate = itlog.Creator,
                LogTextCollection = Regex.Split(itlog.LogText.Substring(0, itlog.LogText.Length - 1), ";").ToList()
            };
            return log;
        }

        public ItDevice GetDeviceById(string id)
        {
            if (id != String.Empty)
            {
                Guid guidid = Guid.Parse(id);
                return _context.ItDevices.FirstOrDefault(x => x.DevItGenId == guidid);
            }
            return null;
        }

        public ItDeviceLog GetLogByDeviceId(string deviceid)
        {
            
            return Itlog.GetLogById(deviceid);


        }

        public IEnumerable<ItDevice> GetDevices()
        {

            return _context.ItDevices.OrderBy(x=>x.DeviceType);

        }

        public IEnumerable<ItDevice> GetDevicesByType(string devicetype)
        {
            if (devicetype != "Все")
            {
                return  _context.ItDevices.Where(x => x.DeviceType == devicetype).OrderBy(x=>x.DeviceType);
            }
            return GetDevices();
        }

        public IEnumerable<ItDevice> GetDevicesByInventory(string invnum)
        {


            return _context.ItDevices.Where(x => x.DevInvNum == invnum);

        }

        public IEnumerable<ItDevice> GetDevicesByOwner(string owner)
        {


            return null;

        }

        public IEnumerable<ItDevice> GetDevicesByLocation(string location)
        {

            
            return _context.ItDevices.Where(x => x.Location == location);

        }

        public IEnumerable<ItDevice> Search(string searchitem)
        {
            
            return _context.ItDevices.Where(x => x.DevInvNum.Contains(searchitem) || x.DevBuhInvNumber.Contains(searchitem) || x.DeviceClass.Contains(searchitem)
                                           || x.DeviceType.Contains(searchitem) || x.DateInWork.Contains(searchitem) || x.DeviceOwner.FullName.Contains(searchitem)
                                           || x.DeviceOwner.Department.Contains(searchitem) || x.DeviceOwner.Job.Contains(searchitem) || x.DeviceOwner.Slugba.Contains(searchitem)
                                           || x.Location.Contains(searchitem) || x.SerialNumber.Contains(searchitem) || x.DeviceModel.Contains(searchitem)).OrderBy(x=>x.DeviceType);


        }
        public IEnumerable<ItDevice> GetDevicesBySerialNumber(string serialnumber)
        {


            return _context.ItDevices.Where(x => x.SerialNumber == serialnumber );


        }

        public bool SaveDevice(ItDevice device, string username)
        {

            
            if (device.DevItGenId == Guid.Empty)
            {
                 device.ItDeviceLog = Itlog.CreateLog(device.DevItGenId, username);
                _context.ItDevices.Add(device);
                _context.SaveChanges();
                
                return true;
            }
            else
            {
                ItDevice currentdevice = _context.ItDevices.FirstOrDefault(x=>x.DevItGenId == device.DevItGenId);
                if (currentdevice != null)
                {

                    currentdevice.DevInvNum = device.DevInvNum;
                    currentdevice.DevBuhInvNumber = device.DevBuhInvNumber;
                    currentdevice.DeviceClass = device.DeviceClass;
                    currentdevice.DeviceType = device.DeviceType;
                    currentdevice.DateInWork = device.DateInWork;
                    currentdevice.SerialNumber = device.SerialNumber;
                    currentdevice.DeviceModel = device.DeviceModel;
                    currentdevice.DeviceOwner.FullName = device.DeviceOwner.FullName;
                    currentdevice.DeviceOwner.Job = device.DeviceOwner.Job;
                    currentdevice.DeviceOwner.Slugba = device.DeviceOwner.Slugba;
                    currentdevice.DeviceOwner.Department = device.DeviceOwner.Department;
                    currentdevice.DeviceOwner.Address = device.DeviceOwner.Address;
                    currentdevice.DeviceOwner.Room = device.DeviceOwner.Room;
                    currentdevice.DeviceOwner.Floor = device.DeviceOwner.Floor;
                    currentdevice.DeviceOwner.Tel = device.DeviceOwner.Tel;
                    currentdevice.Location = device.Location;
                    currentdevice.Comment = device.Comment;

                    currentdevice.ItDeviceLog = Itlog.ChangeLogStatment(device.DevItGenId, username);
                    _context.Entry(currentdevice).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;

                }
                return false;
            }

        }

        
        public bool DeleteDevice(string[] selectids)
        {
            foreach (string id in selectids)
            {
                Guid guidid = Guid.Parse(id);
                ItDevice device = _context.ItDevices.Include("ItDeviceLog").FirstOrDefault(d => d.DevItGenId == guidid);
                if (device != null)
                {
                    _context.ItDeviceLogs.Remove(device.ItDeviceLog);
                    _context.ItDevices.Remove(device);
                    _context.SaveChanges();
                    
                }
            }
            
            return true;

        }


       
    }

}