using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;

namespace Bieb.Tests.Models
{
    [TestFixture]
    public abstract class ModelTests<T> where T : class, new()
    {
        // By default assume all properties need to have a display attribute
        // with associated resource. The base class and its descendants can 
        // optionally exclude certain properties (e.g. because they will be
        // hidden).
        protected List<string> PropertiesNotNeedingDisplay = new List<string>(new[] {"Id", "ModifiedDateTicks"});


        [Test]
        public void All_Public_Model_Properties_Have_Display_Attribute()
        {
            var model = new T();
            foreach (var propertyInfo in model.GetType().GetProperties().Where(pi => !PropertiesNotNeedingDisplay.Contains(pi.Name)))
            {
                var hasProperAttribute = false;

                foreach (var attribute in propertyInfo.CustomAttributes)
                {
                    if (attribute.AttributeType == typeof(DisplayAttribute) 
                        && attribute.NamedArguments !=  null
                        && attribute.NamedArguments.Count(arg => arg.MemberName == "Name") > 0
                        && attribute.NamedArguments.Count(arg => arg.MemberName == "ResourceType") > 0)
                    {
                        hasProperAttribute = true;
                    }
                }

                Assert.That(hasProperAttribute, string.Format("Missing DisplayAttribute (with reqruied Name and ResourceType arguments) for property [{0}]. Either provided an attribute with localized resource, or exclude this property from needing to do so in the test fixture.", propertyInfo.Name));
            }
        }
    }
}
