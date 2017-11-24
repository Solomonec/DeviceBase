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
    public class PaDeviceRepository : IDeviceRepository<PaDevice>
    {
        private readonly DeviceContext _context;

       private ILogRepository<PaDeviceLog> Palog { get; set; }

       public PaDeviceRepository(DeviceContext context)
        {
            this._context = context;
            Palog = new PaLogRepository(context);
        }

        public PaDevice GetDeviceById(string id)
        {
            Guid devid = Guid.Parse(id);
            
            return _context.PaDevices.FirstOrDefault(x => x.DevPaGenId == devid);


        }

        public LogViewModel GetLogById(string id)
        {
            PaDeviceLog palog = Palog.GetLogById(id);
            LogViewModel log = new LogViewModel
            {
                CreateDate = palog.CreationDate,
                ChangeDate = palog.LastUpdateDate,
                WhoChange = palog.WhoLastUpdate,
                WhoCreate = palog.Creator,
                LogTextCollection = Regex.Split(palog.LogText.Substring(0, palog.LogText.Length - 1), ";").ToList()
            };
            return log;
        }

        public IEnumerable<PaDevice> GetDevices()
        {
            return _context.PaDevices.OrderBy(x => x.DeviceType);

        }

        public IEnumerable<PaDevice> GetDevicesByType(string devicetype)
        {

            if (devicetype != "Все")
            {
                return _context.PaDevices.Where(x => x.DeviceType == devicetype).OrderBy(x => x.DeviceType);
            }
            return GetDevices();
        }

        public IEnumerable<PaDevice> GetDevicesByInventory(string invnum)
        {


            return _context.PaDevices.Where(x => x.DevInvNum == invnum);

        }

        public IEnumerable<PaDevice> GetDevicesByOwner(string owner)
        {


            return null;

        }

        public IEnumerable<PaDevice> GetDevicesByLocation(string location)
        {


            return _context.PaDevices.Where(x => x.Location == location);

        }

        public IEnumerable<PaDevice> Search(string searchitem)
        {

            return _context.PaDevices.Where(x => x.DevInvNum.Contains(searchitem) || x.DevBuhInvNumber.Contains(searchitem) || x.DeviceClass.Contains(searchitem)
                                           || x.DeviceType.Contains(searchitem) || x.DateInWork.Contains(searchitem) || x.DeviceOwner.FullName.Contains(searchitem)
                                           || x.DeviceOwner.Department.Contains(searchitem) || x.DeviceOwner.Job.Contains(searchitem) || x.DeviceOwner.Slugba.Contains(searchitem)
                                           || x.Location.Contains(searchitem) || x.SerialNumber.Contains(searchitem) || x.DeviceModel.Contains(searchitem)).OrderBy(x=>x.DeviceType);


        }
        public IEnumerable<PaDevice> GetDevicesBySerialNumber(string serialnumber)
        {


            return _context.PaDevices.Where(x => x.SerialNumber == serialnumber);


        }

        public bool SaveDevice(PaDevice device, string username)
        {

            if (device.DevPaGenId == Guid.Empty)
            {
                device.PaDeviceLog = Palog.CreateLog(device.DevPaGenId, username);
                _context.PaDevices.Add(device);
               _context.SaveChanges();
                

            }
            else
            {
                PaDevice currentdevice = _context.PaDevices.FirstOrDefault(x => x.DevPaGenId == device.DevPaGenId);
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

                }
                 currentdevice.PaDeviceLog = Palog.ChangeLogStatment(device.DevPaGenId, username);
                _context.Entry(currentdevice).State = EntityState.Modified;
                _context.SaveChanges();
                

            }

            _context.SaveChanges();
            return true;
        }


        public bool DeleteDevice(string[] selectids)
        {
            foreach (string id in selectids)
            {
                Guid guidid = Guid.Parse(id);
                PaDevice device = _context.PaDevices.Include("PaDeviceLog").FirstOrDefault(d => d.DevPaGenId == guidid);
                if (device != null)
                {
                    _context.PaDeviceLogs.Remove(device.PaDeviceLog);
                    _context.PaDevices.Remove(device);
                    

                }
            }
            _context.SaveChanges();
            return true;

        }



    }

}