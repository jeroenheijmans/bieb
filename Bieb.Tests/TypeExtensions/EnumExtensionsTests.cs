using System.Linq;
using Bieb.Web.TypeExtensions;
using NUnit.Framework;

namespace Bieb.Tests.TypeExtensions
{
    public class EditorForExtensionsTests
    {
        private enum TestEnum { One, Two }


        [Test]
        public void Enum_As_DropDown_List_Gives_All_Values()
        {
            const TestEnum uno = TestEnum.One;

            var result = uno.ToSelectListItems();

            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public void Enum_As_DropDown_List_Gives_Enum_Names_ToString_As_Values()
        {
            const TestEnum duo = TestEnum.Two;

            var result = duo.ToSelectListItems().ToList();

            Assert.That(result[0].Value, Is.EqualTo("One"));
            Assert.That(result[1].Value, Is.EqualTo("Two"));
        }
    }

}
