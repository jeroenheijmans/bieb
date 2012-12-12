using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace Bieb.Tests.Routing
{
    [TestFixture]
    public class BookRoutingTests : DomainRoutingTests
    {
        protected override string PrimaryAlias
        {
            get { return "LibraryBooks"; }
        }

        protected override IEnumerable<string> Aliases
        {
            get
            {
                return new[] { PrimaryAlias, "Books" };
            }
        }
    }
}
