using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.ViewModels;

namespace DeviceBase.BLL.Interfaces
{
    public interface IStatistics
    {
        IEnumerable<StatisticsResultModel> GetDevicesStatistics(IEnumerable<FilterResultModel> filterresults);
    }
}
