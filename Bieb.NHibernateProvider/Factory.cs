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
            var cfg = GetConfiguration();
            var schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);

            using (TextWriter writer = File.CreateText("BiebDatabase.sql")) 
            {
                schemaExport.Execute(true, executeOnDatabase, false, null, writer);
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            Configuration cfg = GetConfiguration();
            return cfg.BuildSessionFactory();
        }

        private static Configuration GetConfiguration()
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(db =>
            {
                db.ConnectionStringName = "BiebDatabase";
                db.Dialect<MsSql2008Dialect>();
            })
                .AddAssembly(Assembly.GetExecutingAssembly())
                .CurrentSessionContext<WebSessionContext>();
            
            return cfg;
        }
    }
}
