using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bieb.Web.Models;
using Bieb.Web.Models.Series;
using NUnit.Framework;

namespace Bieb.Tests.Models
{
    public class EditSeriesModelTests : ModelTests<EditSeriesModel>
    {
        public EditSeriesModelTests()
        {
            this.PropertiesNotNeedingDisplay.Add("AvailableBooks");
        }
    }
}
