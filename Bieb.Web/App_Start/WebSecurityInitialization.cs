using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Bieb.Web.App_Start
{
    public class WebSecurityInitialization
    {
        private static bool _isInitializing;
        private static readonly object LockObject = new object();

        private const string DefaultUserName = "admin";
        private const string DefaultPassword = "admin";
        private const string AdminRole = "Administrator";

        public static void Initialize()
        {
            if (!_isInitializing)
            {
                lock (LockObject)
                {
                    _isInitializing = true;

                    WebSecurity.InitializeDatabaseConnection("BiebDatabase", "UserProfile", "UserId", "UserName", autoCreateTables: true);

                    if (!WebSecurity.UserExists(DefaultUserName))
                    {
                        WebSecurity.CreateUserAndAccount(DefaultUserName, DefaultPassword);
                    }

                    if (!Roles.RoleExists(AdminRole))
                    {
                        Roles.CreateRole(AdminRole);
                        Roles.AddUserToRole(DefaultUserName, AdminRole);
                    }
                }
            }

        }
    }
}