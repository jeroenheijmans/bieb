using System.Collections.Generic;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Search
{
    public class BasicSearchResultModel
    {
        public string query;
        public IEnumerable<Person> people { get; set; }
        public IEnumerable<Book> books { get; set; }
        public IEnumerable<Story> stories { get; set; }
    }
}