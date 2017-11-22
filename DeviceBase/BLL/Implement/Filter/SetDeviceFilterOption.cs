using System;
using System.Linq;
using System.Linq.Expressions;
using DeviceBase.BLL.Interfaces.Filter;

namespace DeviceBase.BLL.Implement.Filter
{
    public class SetDeviceFilterOption<T>: DeviceFilterOption<T> where T:class 
    {
        public SetDeviceFilterOption(Expression<Func<T, bool>> prepicate, IQueryable<T> devices, DeviceFilter<T> filter)
            : base(prepicate, devices, filter)
        {

        }

        public override IQueryable<T> FilterResult()
        {
            return _filter.FilterResult().Where(_predicate);
        }
    }
}