using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace Bieb.NHibernateProvider
{
    public interface ISessionProvider
    {
        ISession Current { get; }
    }
}
