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
    public class AsppDeviceRepository : IDeviceRepository<AsppDevice>

    {
        private readonly DeviceContext _context;
        [Inject]
        public ILogRepository<AsppDeviceLog> AsppLog { get; set; }
        public AsppDeviceRepository(DeviceContext context)
        {
            this._context = context;
        }

        public AsppDevice GetDeviceById(string id)
        {

            if (id != String.Empty)
            {
                Guid guidid = Guid.Parse(id);
                return _context.AsppDevices.FirstOrDefault(x => x.DevAsppGenId == guidid);
            }
            return null;


        }

        public LogViewModel GetLogById(string id)
        {
            AsppDeviceLog aspplog = AsppLog.GetLogById(id);
            LogViewModel log = new LogViewModel
            {
                CreateDate = aspplog.CreationDate,
                ChangeDate = aspplog.LastUpdateDate,
                WhoChange = aspplog.WhoLastUpdate,
                WhoCreate = aspplog.Creator,
                LogTextCollection = Regex.Split(aspplog.LogText.Substring(0, aspplog.LogText.Length - 1), ";").ToList()
            };
            return log;
        }

        public IEnumerable<AsppDevice> GetDevices()
        {

            return _context.AsppDevices.OrderBy(x => x.DeviceType);
        }

        public IEnumerable<AsppDevice> GetDevicesByType(string devicetype)
        {
            if (devicetype != "Все")
            {
                   return _context.AsppDevices.Where(x => x.DeviceType == devicetype).OrderBy(x=>x.DeviceType);
             
            }
            return GetDevices();
        }

        public IEnumerable<AsppDevice> GetDevicesByInventory(string invnum)
        {


            return _context.AsppDevices.Where(x => x.DevInvNum == invnum);

        }

        public IEnumerable<AsppDevice> GetDevicesByOwner(string owner)
        {


            return null;

        }

        public IEnumerable<AsppDevice> GetDevicesByLocation(string location)
        {

            
            return _context.AsppDevices.Where(x => x.Location == location);

        }

        public IEnumerable<AsppDevice> Search(string searchitem)
        {
            
            return _context.AsppDevices.Where(x => x.DevInvNum.Contains(searchitem) || x.DevBuhInvNumber.Contains(searchitem) || x.DeviceClass.Contains(searchitem)
                                           || x.DeviceType.Contains(searchitem) || x.DateInWork.Contains(searchitem) || x.DeviceOwner.FullName.Contains(searchitem)
                                           || x.DeviceOwner.Department.Contains(searchitem) || x.DeviceOwner.Job.Contains(searchitem) || x.DeviceOwner.Slugba.Contains(searchitem)
                                           || x.Location.Contains(searchitem) || x.SerialNumber.Contains(searchitem) || x.DeviceModel.Contains(searchitem)).OrderBy(x => x.DeviceType);


        }
        public IEnumerable<AsppDevice> GetDevicesBySerialNumber(string serialnumber)
        {


            return _context.AsppDevices.Where(x => x.SerialNumber == serialnumber);


        }

        public bool SaveDevice(AsppDevice device, string username)
        {

            if (device.DevAsppGenId == Guid.Empty)
            {
                _context.AsppDevices.Add(device);
                _context.SaveChanges();
                _context.AsppDeviceLogs.Add(AsppLog.CreateLog(device.DevAsppGenId, username));
                _context.SaveChanges();
                return true;
            }
            else
            {
                AsppDevice currentdevice = _context.AsppDevices.FirstOrDefault(x => x.DevAsppGenId == device.DevAsppGenId);
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

                    _context.Entry(currentdevice).State = EntityState.Modified;
                    _context.SaveChanges();
                    _context.Entry(AsppLog.ChangeLogStatment(device.DevAsppGenId, username)).State = EntityState.Modified;
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
                AsppDevice device = _context.AsppDevices.Include("AsppDeviceLog").FirstOrDefault(d => d.DevAsppGenId == guidid);
                if (device != null)
                {
                    _context.AsppDeviceLogs.Remove(device.AsppDeviceLog);
                    _context.AsppDevices.Remove(device);
                  

                }
            }
            _context.SaveChanges();
            return true;

        }


       
    }

}