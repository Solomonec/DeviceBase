using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.Models;

namespace DeviceBase.Code.Interfaces
{
    public interface ILogRepository<T>
    {
        T GetLogById(string id);
        T CreateLog(Guid id, string username);
        T ChangeLogStatment(Guid id, string username);
        bool SaveDeviceLog(T log);
        bool DeleteDeviceLog(T log);
                        

    }
}