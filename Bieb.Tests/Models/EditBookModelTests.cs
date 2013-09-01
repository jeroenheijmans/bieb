using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Web.Models;

namespace Bieb.Tests.Models
{
    public class EditBookModelTests : ModelTests<EditBookModel>
    {
        public EditBookModelTests()
        {
            this.PropertiesNotNeedingDisplay.Add("AvailablePublishers");
            this.PropertiesNotNeedingDisplay.Add("AvailablePeople");
        }
    }
}
