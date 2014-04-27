using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bieb.NHibernateProvider;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests
{
    public class DatabaseIntegrationTest
    {
        private static Configuration _configuration;
        private static ISessionFactory _factory;
        protected ISession Session;

        public DatabaseIntegrationTest()
        {
            if (_configuration == null)
            {
                log4net.Config.XmlConfigurator.Configure();

                _configuration = new Configuration();

                _configuration.DataBaseIntegration(db =>
                                                       {
                                                           db.LogFormattedSql = true;
                                                           db.ConnectionStringName = "BiebDbIntegrationTests";
                                                           db.Dialect<MsSql2008Dialect>();
                                                       })
                              .SetProperty(NHibernate.Cfg.Environment.CollectionTypeFactoryClass, typeof (Net4CollectionTypeFactory).AssemblyQualifiedName)
                              .AddAssembly(typeof(Bieb.NHibernateProvider.Factory).Assembly)
                              .CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>();

                _factory = _configuration.BuildSessionFactory();
            }

            using (var schemaCreationSession = _factory.OpenSession())
            {
                new SchemaExport(_configuration).Execute(true, true, false, schemaCreationSession.Connection, Console.Out);
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


        public void Dispose()
        {
            Session.Dispose();
        }

    }
}
