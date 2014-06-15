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
                NHibernateProvider.FactoryProvider.CreateSchema(false);
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
