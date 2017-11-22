using System;
using System.Linq;
using System.Linq.Expressions;

namespace DeviceBase.BLL.Interfaces.Filter
{
    public abstract class DeviceFilterOption<T>: DeviceFilter<T> 
    {
        protected DeviceFilter<T> _filter;
        protected Expression<Func<T, bool>> _predicate; 
        protected DeviceFilterOption(Expression<Func<T,bool>> predicate, IQueryable<T> devices, DeviceFilter<T> filter) : base(devices)
        {
            _filter = filter;
            _predicate = predicate;
        }

        
    }
}