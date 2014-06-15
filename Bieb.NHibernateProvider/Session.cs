using System;
using NHibernate;

namespace Bieb.NHibernateProvider
{
    public class SessionProvider : ISessionProvider, IDisposable
    {
        public SessionProvider(IFactoryProvider factoryProvider)
        {
            Current = factoryProvider.Current.OpenSession();
        }

        public ISession Current { get; private set; }

        public void Dispose()
        {
            if (Current != null && Current.IsOpen)
            {
                Current.Close();
            }
        }
    }
}
