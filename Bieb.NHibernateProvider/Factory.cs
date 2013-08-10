using NHibernate.Cfg;
using NHibernate;
using NHibernate.Dialect;
using System.Reflection;
using NHibernate.Context;
using System.IO;
using System.Diagnostics;

namespace Bieb.NHibernateProvider
{
    public static class Factory
    {
        static Factory() 
        {
            _instance = CreateSessionFactory();
        }

        private static ISessionFactory _instance;
        public static ISessionFactory Instance
        {
            get
            {
                Debug.Assert(_instance != null, "Session factory was not yet created, even though it should have been created in the static constructor.");
                return _instance;
            }
        }

        public static void CreateSchema(bool executeOnDatabase)
        {
            var configuration = GetConfiguration();
            var schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(configuration);

            using (TextWriter writer = File.CreateText("BiebDatabase.sql")) 
            {
                schemaExport.Execute(true, executeOnDatabase, false, null, writer);
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = GetConfiguration();
            return configuration.BuildSessionFactory();
        }

        private static Configuration GetConfiguration()
        {
            var configuration = new Configuration();

            configuration.DataBaseIntegration(db =>
                                        {
                                            db.LogFormattedSql = true;
                                            db.ConnectionStringName = "BiebDatabase";
                                            db.Dialect<MsSql2008Dialect>();
                                        })
               .AddAssembly(Assembly.GetExecutingAssembly())
               .CurrentSessionContext<WebSessionContext>();

            return configuration;
        }
    }
}
