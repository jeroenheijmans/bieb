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

        private const string SqlSetupCommand = @"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{0}')
                                                 BEGIN
                                                     ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                                     DROP DATABASE [{0}];
                                                 END
                                             
                                                 DECLARE @FileName AS VARCHAR(MAX) = CAST(SERVERPROPERTY('instancedefaultdatapath') AS VARCHAR(MAX)) + '{0}';
                                             
                                                 EXEC ('CREATE DATABASE [{0}] ON PRIMARY (NAME = [{0}], FILENAME = ''' + @FileName + ''')');";


        private Factory()
        {
            log4net.Config.XmlConfigurator.Configure();
            
            using (var masterConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["master"].ConnectionString))
            {
                masterConnection.Open();
                var databaseName = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["tests"].ConnectionString).InitialCatalog;
                var sql = string.Format(SqlSetupCommand, databaseName);
                var cmd = new SqlCommand(sql, masterConnection);
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
