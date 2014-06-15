using NHibernate;

namespace Bieb.DataAccess
{
    public interface IFactoryProvider
    {
        ISessionFactory Current { get; }
    }
}