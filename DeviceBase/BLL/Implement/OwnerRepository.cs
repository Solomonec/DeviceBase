using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Models;

namespace DeviceBase.BLL.Implement
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DeviceContext _context;

        public OwnerRepository(DeviceContext context)
        {
            _context = context;
        }


        public IQueryable<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(x=>x.Name);
        }

        public Owner GetOwnerById(string ownerid)
        {
            if (ownerid != String.Empty)
            {
                Guid guidid = Guid.Parse(ownerid);
                return _context.Owners.FirstOrDefault(x => x.OwnerId == guidid);
            }
            return null;
        }

        public List<string> GetOwnersDepartments()
        {

            var groupresults = _context.Owners.GroupBy(x => x.Slugba).Select(g => new { Name = g.Key });

            List<string> resultvalues = new List<string>();
            foreach (var item in groupresults)
            {
                resultvalues.Add(item.Name);
            }
            return resultvalues;
        }

        public IQueryable<Owner> GetOwnersByName(string ownername)
        {
            return _context.Owners.Where(x => x.Name.Contains(ownername) || x.FullName.Contains(ownername)).OrderBy(x => x.Name);
        }

        public bool CreateNewOwner(Owner owner)
        {
            Guid newidGuid = Guid.Empty;
            if (owner.OwnerId == newidGuid)
            {
                _context.Owners.Add(owner);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveOwner(Owner owner)
        {
            Owner currentowner = _context.Owners.FirstOrDefault(x => x.OwnerId == owner.OwnerId);
            if (currentowner != null)
            {
                currentowner.Name = owner.Name;
                currentowner.FullName = owner.FullName;
                currentowner.Job = owner.Job;
                currentowner.Slugba = owner.Slugba;
                currentowner.Department = owner.Department;
                currentowner.Address = owner.Address;
                currentowner.Floor = owner.Floor;
                currentowner.Room = owner.Room;
                currentowner.Tel = owner.Tel;
                _context.Entry(currentowner).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteOwners(string[] ownerids)
        {
            if (ownerids != null)
            {
                foreach (string id in ownerids)
                {
                    Guid guidid = Guid.Parse(id);
                    Owner owner = _context.Owners.FirstOrDefault(x => x.OwnerId == guidid);
                    _context.Entry(owner).State = EntityState.Deleted;
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}