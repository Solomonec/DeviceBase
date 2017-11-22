using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;
using Microsoft.Ajax.Utilities;

namespace DeviceBase.Code.Implement
{
    public class PaLogRepository:ILogRepository<PaDeviceLog>
    {
        private readonly DeviceContext _context;
        public PaLogRepository(DeviceContext context)
        {
            this._context = context;
        }

        public PaDeviceLog GetLogById(string id)
        {

            Guid nlogid = Guid.Parse(id);
            var log = _context.PaDeviceLogs.FirstOrDefault(x => x.DevPaGenId == nlogid);
            return log;
        
        }
        public PaDeviceLog CreateLog(Guid id, string username)
        {
            PaDeviceLog nlog = new PaDeviceLog
            {
                DevPaGenId = id,
                CreationDate = DateTime.Now,
                Creator = username,
                LastUpdateDate = DateTime.Now,
                WhoLastUpdate = username,
                LogText = DateTime.Now.ToString() + " Пользователь " + username + " создал новое устройство;"
            };

            return nlog;
        }
       public PaDeviceLog ChangeLogStatment(Guid id, string username)
        {
            PaDeviceLog log = _context.PaDeviceLogs.FirstOrDefault(x => x.DevPaGenId == id);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = username;
            log.LogText += DateTime.Now.ToString() + " Пользователь " + username + " внес изменения в информацию об устройстве;";
            return log;
        }
       public bool SaveDeviceLog(PaDeviceLog log)
        {
            return true;
        }

       public bool DeleteDeviceLog(PaDeviceLog log)
       {
           return true;
       }
    }
}