using NHibernate.Cfg;
using NHibernate;
using NHibernate.Dialect;
using System.Reflection;
using NHibernate.Context;
using System.IO;

namespace Bieb.NHibernateProvider
{
    public sealed class Factory
    {
        private Factory() { }

        private static ISessionFactory _instance;
        public static ISessionFactory Instance
        {
            get
            {
                return _instance ?? (_instance = CreateSessionFactory());
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
