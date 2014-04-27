using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.DbIntegrationTests.DatabaseObjects
{
    [TestFixture]
    public class BookDatabaseObjectsTests : PublishableEntityDatabaseObjectsTests<Book>
    {
    }
}
