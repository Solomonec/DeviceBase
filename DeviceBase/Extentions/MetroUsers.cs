using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.Models;

namespace DeviceBase.Extentions
{
    public static class MetroUsers
    {

        public static UserProfile GetProfileByUserName(string username)
        {
            using (UserContext context = new UserContext())
            {
                return context.UserProfiles.FirstOrDefault(x => x.UserName == username);
            }
        }
    }
}