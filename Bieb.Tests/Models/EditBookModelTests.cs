using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bieb.Web.Models.Books;

namespace Bieb.Tests.Models
{
    public class EditBookModelTests : ModelTests<EditBookModel>
    {
        public EditBookModelTests()
        {
            this.PropertiesNotNeedingDisplay.Add("AvailablePublishers");
            this.PropertiesNotNeedingDisplay.Add("AvailablePeople");
            this.PropertiesNotNeedingDisplay.Add("AvailableIsbnLanguages");
            this.PropertiesNotNeedingDisplay.Add("AvailableLibraryStatuses");
        }
    }
}
