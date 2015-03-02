using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using NUnit.Framework;

namespace Bieb.Tests.Localization
{
    [TestFixture]
    public class EnumDisplayerTests
    {
        private readonly IEnumerable<LibraryStatus> libraryStatuses = Enum.GetValues(typeof(LibraryStatus)).Cast<LibraryStatus>();
        private readonly IEnumerable<Gender> genders = Enum.GetValues(typeof (Gender)).Cast<Gender>();

        
        [Test]
        public void Can_Display_LibraryStatus_OnlyForReference()
        {
            var resource = EnumDisplayer.GetResource(LibraryStatus.OnlyForReference);

            // We don't care so much about casing, just that it's not the enum value ToString(), i.e. we're looking for a space in the string...
            Assert.That(resource.ToLower(), Is.EqualTo("just for reference"));
        }


        [Test]
        public void Will_Display_Text_For_Any_LibraryStatus()
        {
            var texts = libraryStatuses.Select(EnumDisplayer.GetResource);
            Assert.That(texts, Is.All.Not.Null.And.Not.Empty);
        }


        [Test]
        public void Can_Display_Gender_Male_Symbol()
        {
            var resource = EnumDisplayer.GetResource(Gender.Male);
            Assert.That(resource, Is.EqualTo("♂"));
        }


        [Test]
        public void Will_Display_Text_For_Any_Gender()
        {
            var texts = genders.Select(EnumDisplayer.GetResource);
            Assert.That(texts, Is.All.Not.Null.And.Not.Empty);
        }
    }
}
