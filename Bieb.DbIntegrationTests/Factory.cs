using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.DataAccess;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

namespace Bieb.DbIntegrationTests
{
    // Based on: http://csharpindepth.com/Articles/General/Singleton.aspx
    public sealed class Factory
    {
        private static readonly Lazy<Factory> lazy = new Lazy<Factory>(() => new Factory());
        public static Factory Instance { get { return lazy.Value; } }

        private static ISessionFactory _factory;

        private const string SqlSetupCommand = @"IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Tests')
                                                 BEGIN
                                                     ALTER DATABASE [Tests] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                                     DROP DATABASE [Tests];
                                                 END
                                             
                                                 DECLARE @FileName AS VARCHAR(MAX) = CAST(SERVERPROPERTY('instancedefaultdatapath') AS VARCHAR(MAX)) + 'Tests';
                                             
                                                 EXEC ('CREATE DATABASE [Tests] ON PRIMARY (NAME = [Tests], FILENAME = ''' + @FileName + ''')');";


        private Factory()
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
                .AddAssembly(typeof(FactoryProvider).Assembly)
                .CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>();

            _factory = configuration.BuildSessionFactory();


            using (var schemaCreationSession = _factory.OpenSession())
            {
                new SchemaExport(configuration).Execute(true, true, false, schemaCreationSession.Connection, Console.Out);
            }
        }

        public ISession OpenSession()
        {
            return _factory.OpenSession();
        }
    }
}
