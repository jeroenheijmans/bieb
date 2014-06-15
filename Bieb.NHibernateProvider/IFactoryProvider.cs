using NHibernate;

namespace Bieb.NHibernateProvider
{
    public interface IFactoryProvider
    {
        ISessionFactory Current { get; }
    }
}