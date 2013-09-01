using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Web.Models;

namespace Bieb.Tests.Models
{
    public class EditPersonModelTests : ModelTests<EditPersonModel>
    {
        public EditPersonModelTests()
        {
            this.PropertiesNotNeedingDisplay.Add("FullName");
        }
    }
}
