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
            // Arrange
            const TestEnum uno = TestEnum.One;

            // Act
            var result = uno.ToSelectListItems();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Enum_As_DropDown_List_Gives_Enum_Names_ToString_As_Values()
        {
            // Arrange
            const TestEnum duo = TestEnum.Two;

            // Act
            var result = duo.ToSelectListItems().ToList();

            // Assert
            Assert.That(result[0].Value, Is.EqualTo("One"));
            Assert.That(result[1].Value, Is.EqualTo("Two"));
        }
    }

}
