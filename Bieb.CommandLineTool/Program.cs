using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.CommandLineTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("CreateSchema"))
            {
                log4net.Config.XmlConfigurator.Configure();

                log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
                log.Info("TESTING!!");

                Bieb.NHibernateProvider.Factory.CreateSchema();
                Console.WriteLine("Press enter to finish");
                Console.ReadLine();
            }
        }
    }
}
