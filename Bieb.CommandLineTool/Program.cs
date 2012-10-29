using System;
using System.Linq;

namespace Bieb.CommandLineTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("CreateSchema"))
            {
                log4net.Config.XmlConfigurator.Configure();

                NHibernateProvider.Factory.CreateSchema(false);
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No valid arguments provided. Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
