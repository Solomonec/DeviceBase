using System.Linq;

namespace DeviceBase.BLL.Interfaces.Filter
{
    public abstract class DeviceFilter<T>
    {
        protected IQueryable<T> _devices { get; set; }

        protected DeviceFilter(IQueryable<T> devices)
        {
            _devices = devices;
        }
        
        public abstract IQueryable<T> FilterResult();
        
    }
}