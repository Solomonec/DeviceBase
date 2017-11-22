using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.BLL.Interfaces;
using DeviceBase.ViewModels;
using DeviceBase.Models;

namespace DeviceBase.BLL.Implement
{
    class Statistics:IStatistics
    {
        private readonly DeviceContext _context;

        public Statistics (DeviceContext context)
        {
            _context = context;
        }

        public IEnumerable<StatisticsResultModel> GetDevicesStatistics(IEnumerable<FilterResultModel> filterresults)
        {

            var groupresults = filterresults.GroupBy(x => x.DeviceType).Select(g => new { Name = g.Key, Quantity = g.Count() });

            List<StatisticsResultModel> resultvalues = new List<StatisticsResultModel>();
            foreach (var item in groupresults)
            {
                StatisticsResultModel statisticsitem = new StatisticsResultModel();
                statisticsitem.DeviceType = item.Name;
                statisticsitem.Quantity = item.Quantity;
                resultvalues.Add(statisticsitem);
            }
            return resultvalues;
        }
    }
}
