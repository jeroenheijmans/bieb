using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Bieb.DataAccess;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests
{
    public class DatabaseIntegrationTest
    {
        protected ISession Session;
        
        [SetUp]
        public virtual void SetUp()
        {
            if (Session != null && Session.IsOpen)
            {
                Session.Close();
            }

            Session = Factory.Instance.OpenSession();
        }

        [TearDown]
        public void TearDown()
        {
            Session.Dispose();
        }
    }
}
