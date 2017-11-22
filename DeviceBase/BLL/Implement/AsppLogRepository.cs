using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;
using Microsoft.Ajax.Utilities;
using Ninject;

namespace DeviceBase.Code.Implement
{
    public class AsppLogRepository:ILogRepository<AsppDeviceLog>
    {
        private DeviceContext _context;

      
        public AsppLogRepository(DeviceContext context)
        {
            _context = context;
        }

        public AsppDeviceLog GetLogById(string id)
        {

            Guid nlogid = Guid.Parse(id);
            var log = _context.AsppDeviceLogs.FirstOrDefault(x => x.DevAsppGenId == nlogid);
            return log;
        
        }
        public AsppDeviceLog CreateLog(Guid id, string username)
        {
            AsppDeviceLog nlog = new AsppDeviceLog();
            nlog.DevAsppGenId = id;
            nlog.CreationDate = DateTime.Now;
            nlog.Creator = username;
            nlog.LastUpdateDate = DateTime.Now;
            nlog.WhoLastUpdate = username;
            nlog.LogText = DateTime.Now.ToString() + " " + username + " создал новое устройство;";

            return nlog;
        }
        public AsppDeviceLog ChangeLogStatment(Guid id, string username)
        {
            AsppDeviceLog log = _context.AsppDeviceLogs.FirstOrDefault(x => x.DevAsppGenId == id);
            if (log != null)
            {
                log.LastUpdateDate = DateTime.Now;
                log.WhoLastUpdate = username;
                log.LogText += DateTime.Now.ToString() + " " + username + " внес изменения в информацию об устройстве;";
                return log;
            }
            return null;
           
        }
       public bool SaveDeviceLog(AsppDeviceLog log)
        {
            return true;
        }

       public bool DeleteDeviceLog(AsppDeviceLog log)
       {
           return true;
       }
    }
}