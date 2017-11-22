using System.Linq;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Extentions;
using DeviceBase.Models;
using DeviceBase.ViewModels;

namespace DeviceBase.BLL.Implement
{
    public class DataFilter:IDataFilter
    {
        private readonly DeviceContext _context;

        public DataFilter(DeviceContext context)
        {
            _context = context;

        }

        public IQueryable<FilterResultModel> Filter(FilterEntry entry, string deviceclass, string devicetype, string department, string location)
        {
            switch (entry)
            {
                case FilterEntry.It: return new ItDeviceFilter(_context).Filter(deviceclass,devicetype, department, location).ToFilterResultModel();
                case FilterEntry.Aspp: return new AsppDeviceFilter(_context).Filter(deviceclass, devicetype, department, location).ToFilterResultModel();
                case FilterEntry.Pa: return new PaDeviceFilter(_context).Filter(deviceclass, devicetype, department, location).ToFilterResultModel();
                default: return null;
            }

        }


     
    }

    public enum FilterEntry
    {
        It,
        Aspp,
        Pa
    }
}