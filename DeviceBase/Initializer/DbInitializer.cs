using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using DeviceBase.Models;

namespace DeviceBase.Initializer
{
    public static class DbInitializer
    {

        public static void CreateOrInitializeAllDbs()
        {


            using (UserContext userscontext = new UserContext())
            {
                if (!userscontext.Database.Exists())
                {
                    ((IObjectContextAdapter)userscontext).ObjectContext.CreateDatabase();
                }
            }

            WebSecurity.InitializeDatabaseConnection("DeviceBaseAccountConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                   
                 

            Database.SetInitializer<DeviceContext>(null);

                    using (var context = new DeviceContext())
                    {
                        if (!context.Database.Exists())
                        {
                            context.Database.Create();
                        }
                    }
            
        
        }


    }
}
