using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;
using Microsoft.Ajax.Utilities;

namespace DeviceBase.Code.Implement
{
    public class ItLogRepository:ILogRepository<ItDeviceLog>
    {
        private readonly DeviceContext _context;
        public ItLogRepository(DeviceContext context)
        {
            _context = context;
        }

        public ItDeviceLog GetLogById(string id)
        {
            Guid nlogid = Guid.Parse(id);
            var log = _context.ItDeviceLogs.FirstOrDefault(x => x.DevItGenId == nlogid);
            return log;
        
        }
        public ItDeviceLog CreateLog(Guid id, string username)
        {
            ItDeviceLog nlog = new ItDeviceLog();
            nlog.DevItGenId = id;
            nlog.CreationDate = DateTime.Now;
            nlog.Creator = username;
            nlog.LastUpdateDate = DateTime.Now;
            nlog.WhoLastUpdate = username;
            nlog.LogText = DateTime.Now.ToString() + " " + username + " создал новое устройство;";
            
            return nlog;
        }

       public ItDeviceLog ChangeLogStatment(Guid id, string username)
        {
            ItDeviceLog log = _context.ItDeviceLogs.FirstOrDefault(x => x.DevItGenId == id);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = username;
            log.LogText += DateTime.Now.ToString() + " Пользователь " + username + " внес изменения в информацию об устройстве;";
            return log;
           
        }
       public bool SaveDeviceLog(ItDeviceLog log)
        {
            return true;
        }

       public bool DeleteDeviceLog(ItDeviceLog log)
       {
           return true;
       }
    }
}