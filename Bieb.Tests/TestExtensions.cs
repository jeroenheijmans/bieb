using System;
using System.Collections.Generic;
using System.Linq;

namespace Bieb.Tests
{
    public static class TestExtensions
    {
        /// <summary>
        /// Analogous to the First() extension method, this one returns the Second element
        /// </summary>
        public static T Second<T>(this IEnumerable<T> source)
        {
            return source.Skip(1).First();
        }
    }
}
