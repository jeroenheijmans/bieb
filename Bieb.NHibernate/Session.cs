﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NHibernate;

namespace Bieb.NHibernateProvider
{
    public class Session
    {
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
