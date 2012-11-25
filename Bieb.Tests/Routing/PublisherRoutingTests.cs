using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public class PublisherRoutingTests : DomainRoutingTests
    {
        protected override string PrimaryAlias
        {
            get { return "Publishers"; }
        }
    }
}
