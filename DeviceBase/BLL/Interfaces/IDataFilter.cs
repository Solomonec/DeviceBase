using System.Linq;
using DeviceBase.BLL.Implement;
using DeviceBase.ViewModels;

namespace DeviceBase.BLL.Interfaces
{
    public interface IDataFilter
    {

        IQueryable<FilterResultModel> Filter(FilterEntry entry, string deviceclass, string devicetype, string department, string location);


    }
}