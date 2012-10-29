using System.Web;
using NHibernate;

namespace Bieb.NHibernateProvider
{
    public sealed class Session
    {
        private Session() { }

        // TODO: Refactor this to a DI approach
        internal static ISession Instance
        {
            get
            {
                var session = (ISession)HttpContext.Current.Items["NHibernateSession"];
                if (session == null) {
                    session = Factory.Instance.OpenSession();
                    HttpContext.Current.Items.Add("NHibernateSession", session);
                }
                return session;
            }
        }

        public static void CloseSession()
        {
            var session = (ISession)HttpContext.Current.Items["NHibernateSession"];
            if (session != null)
            {
                if (session.IsOpen)
                {
                    session.Close();
                }
                HttpContext.Current.Items.Remove("NHibernateSession");
            }
        }
    }
}
