using System;
using System.Data.Entity;
using System.Linq;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Models;

namespace DeviceBase.BLL.Implement
{
    public class UserRepository:IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public IQueryable<UserProfile> GetUsers()
        {
            return _context.UserProfiles.OrderBy(x=>x.FullName);
        }

        public UserProfile GetUserById(string id)
        {

            return _context.UserProfiles.FirstOrDefault(x => x.UserId.ToString() == id);
        }

        public UserProfile GetUserByName(string username)
        {
            return _context.UserProfiles.FirstOrDefault(x => x.UserName == username);
        }

        public UserProfile SaveUserProfile(UserProfile profile)
        {
            if (profile.UserId.ToString() != String.Empty)
            {
                UserProfile userProfile = _context.UserProfiles.FirstOrDefault(x => x.UserId == profile.UserId);
                if (userProfile != null)
                {
                    userProfile.FullName = profile.FullName;
                    userProfile.Job = profile.Job;
                    userProfile.Slugba = profile.Slugba;
                    userProfile.Department = profile.Department;
                    userProfile.Tel = profile.Tel;
                    userProfile.Active = profile.Active;
                    _context.Entry(userProfile).State = EntityState.Modified;
                    _context.SaveChanges();
                    return userProfile;
                }
                return null;
            }
            return null;
        }
    }
}