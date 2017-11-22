using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Models;

namespace DeviceBase.BLL.Implement
{
    public class LocationRepository:ILocationRepository
    {
        private readonly DeviceContext _context;

        public LocationRepository(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<Location> GetLocations()
        {
            return _context.Locations.OrderBy(x=>x.LocationName);
        }

        public Location GetLocationById(string locationid)
        {
            if (locationid != String.Empty)
            {
                Guid guidid = Guid.Parse(locationid);
                return _context.Locations.FirstOrDefault(x => x.LocationId == guidid);
            }
            return null;
        }

        public bool CreateNewLocation(Location location)
        {
            Guid newidGuid = Guid.Empty;
            if (location.LocationId == newidGuid)
            {
                _context.Locations.Add(location);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveLocation(Location location)
        {
            Location currentlocation = _context.Locations.FirstOrDefault(x => x.LocationId == location.LocationId);
            if (currentlocation != null)
            {
                currentlocation.LocationName = location.LocationName;
                _context.Entry(currentlocation).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteLocations(string[] locationsid)
        {
            if (locationsid != null)
            {
                foreach (string id in locationsid)
                {
                    Guid guidid = Guid.Parse(id);
                    Location location = _context.Locations.FirstOrDefault(x => x.LocationId == guidid);
                    _context.Entry(location).State = EntityState.Deleted;
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}