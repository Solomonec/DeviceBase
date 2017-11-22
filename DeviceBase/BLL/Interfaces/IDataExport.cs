﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DeviceBase.BLL.Interfaces
{
    public interface IDataExport
    {
        byte[] ExportTo(IExport export, DataTable data);
    }
}