using System.Collections.Generic;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Search
{
    public class BasicSearchResultModel
    {
        public string Query { get; set; }
        public IEnumerable<LinkablePersonModel> People { get; set; }
        public IEnumerable<LinkableBookModel> Books { get; set; }
        public IEnumerable<LinkableStoryModel> Stories { get; set; }
    }
}