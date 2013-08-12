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
    public class BookModelTests
    {
        [Test]
        public void Constructor_for_Domain_Item_Sets_Base_Domain_Properties_on_Model()
        {
            var book = new LibraryBook { Id = 42, ModifiedDate = DateTime.Now };

            var model = new BookModel(book);

            Assert.That(model.Id, Is.EqualTo(book.Id));
            Assert.That(model.ModifiedDateTicks.HasValue);
            Assert.That(model.ModifiedDateTicks.Value, Is.EqualTo(book.ModifiedDate.Value.Ticks));
        }
    }
}
