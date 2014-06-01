using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bieb.Web.Models
{
    public class MappingException : Exception
    {
        public MappingException(string message) : base(message)
        { }
    }
}