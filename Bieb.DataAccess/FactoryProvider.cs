using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;

namespace Bieb.DataAccess
{
    /// <summary>
    /// This class is expensive to create, it's preferred to create it as a singleton in the composition root.
    /// </summary>
    public class FactoryProvider : IFactoryProvider
    {
        public FactoryProvider()
        {
            var configuration = GetConfiguration();
            Current = configuration.BuildSessionFactory();
        }

        public ISessionFactory Current { get; private set; }

        public static void CreateSchema(bool executeOnDatabase)
        {
            var configuration = GetConfiguration();
            var schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(configuration);

            using (TextWriter writer = File.CreateText("BiebDatabase.sql")) 
            {
                if (executeOnDatabase)
                {
                    using (var schemaCreationSession = configuration.BuildSessionFactory().OpenSession())
                    {
                        schemaExport.Execute(true, true, false, schemaCreationSession.Connection, writer);
                    }
                }
                else
                {
                    schemaExport.Execute(true, false, false, null, writer);
                }
            }
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
