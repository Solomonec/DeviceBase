using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Interfaces;

namespace DeviceBase.BLL.Implement
{
    public class DataExport:IDataExport
    {
        
        public byte[] ExportTo(IExport export, DataTable data)
        {
            return export.Export(data);
        }
    }
}