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
        private ISessionFactory _factory;
        protected ISession Session;

        private const string SqlSetupCommand = @"IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Tests')
                                                 BEGIN
                                                     ALTER DATABASE [Tests] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                                     DROP DATABASE [Tests];
                                                 END
                                             
                                                 DECLARE @FileName AS VARCHAR(MAX) = CAST(SERVERPROPERTY('instancedefaultdatapath') AS VARCHAR(MAX)) + 'Tests';
                                             
                                                 EXEC ('CREATE DATABASE [Tests] ON PRIMARY (NAME = [Tests], FILENAME = ''' + @FileName + ''')');";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            log4net.Config.XmlConfigurator.Configure();

            using (var masterConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["master"].ConnectionString))
            {
                masterConnection.Open();
                var cmd = new SqlCommand(SqlSetupCommand, masterConnection);
                cmd.ExecuteNonQuery();
            }

            var configuration = new NHibernate.Cfg.Configuration();

            configuration.DataBaseIntegration(db =>
            {
                db.LogFormattedSql = true;
                db.ConnectionStringName = "tests";
                db.Dialect<MsSql2008Dialect>();
            })
                .AddAssembly(typeof (FactoryProvider).Assembly)
                .CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>();

            _factory = configuration.BuildSessionFactory();


            using (var schemaCreationSession = _factory.OpenSession())
            {
                new SchemaExport(configuration).Execute(true, true, false, schemaCreationSession.Connection, Console.Out);
            }
        }

        [SetUp]
        public virtual void SetUp()
        {
            if (Session != null && Session.IsOpen)
            {
                Session.Close();
            }

            Session = _factory.OpenSession();
        }


        [TearDown]
        public void TearDown()
        {
            Session.Dispose();
        }
    }
}
