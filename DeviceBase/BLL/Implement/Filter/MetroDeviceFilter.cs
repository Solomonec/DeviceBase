using System.Linq;
using DeviceBase.BLL.Interfaces.Filter;

namespace DeviceBase.BLL.Implement.Filter
{
    public class MetroDeviceFilter<T>:DeviceFilter<T> where T:class
    {
        public MetroDeviceFilter(IQueryable<T> devices): base(devices)
        {

        }

        public override IQueryable<T> FilterResult()
        {
            return _devices;
        }
    }
}