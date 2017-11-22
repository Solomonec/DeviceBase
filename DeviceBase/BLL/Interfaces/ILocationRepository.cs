using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.Models;

namespace DeviceBase.BLL.Interfaces
{
    public interface ILocationRepository
    {
        IQueryable<Location> GetLocations();
        Location GetLocationById(string locationid);
        bool CreateNewLocation(Location location);
        bool SaveLocation(Location location);
        bool DeleteLocations(string[] locationsid);
    }
}
