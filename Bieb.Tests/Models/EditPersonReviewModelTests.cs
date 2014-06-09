using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Web.Models.People;

namespace Bieb.Tests.Models
{
    public class EditPersonReviewModelTests : ModelTests<EditPersonReviewModel>
    {
        public EditPersonReviewModelTests()
        {
            PropertiesNotNeedingDisplay.Add("PersonId");
        }
    }
}
