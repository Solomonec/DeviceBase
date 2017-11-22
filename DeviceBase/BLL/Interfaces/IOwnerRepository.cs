using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.Models;

namespace DeviceBase.BLL.Interfaces
{
    public interface IOwnerRepository
    {
        IQueryable<Owner> GetOwners();
        Owner GetOwnerById(string ownerid);
        List<string> GetOwnersDepartments();
        IQueryable<Owner> GetOwnersByName( string ownername);
        bool CreateNewOwner(Owner owner);
        bool SaveOwner(Owner owner);
        bool DeleteOwners(string[] ownerids);

    }
}
