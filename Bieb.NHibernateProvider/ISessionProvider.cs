using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace Bieb.DataAccess
{
    public interface ISessionProvider
    {
        ISession Current { get; }
    }
}
