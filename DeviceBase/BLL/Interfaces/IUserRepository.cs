using System.Linq;
using DeviceBase.Models;

namespace DeviceBase.BLL.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<UserProfile> GetUsers();
        UserProfile GetUserById(string id);
        UserProfile GetUserByName(string username);
        UserProfile SaveUserProfile(UserProfile profile);
    }
}