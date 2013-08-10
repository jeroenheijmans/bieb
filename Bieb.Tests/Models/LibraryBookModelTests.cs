using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using NUnit.Framework;

namespace Bieb.Tests.Models
{
    [TestFixture]
    public class LibraryBookModelTests
    {
        [Test]
        public void Constructor_for_Domain_Item_Sets_Base_Domain_Properties_on_Model()
        {
            // Arrange
            var id = 42;
            var modified = DateTime.Now;
            var book = new LibraryBook {Id = id, ModifiedDate = modified};

            // Act
            var model = new LibraryBookModel(book);

            // Assert
            Assert.That(model.Id, Is.EqualTo(id));
            Assert.That(model.ModifiedDateTicks.HasValue);
            Assert.That(model.ModifiedDateTicks.Value, Is.EqualTo(modified.Ticks));
        }
    }
}
