using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Publishers
{
    public class ViewPublisherModel : ViewEntityModel<Publisher>
    {
        public ViewPublisherModel(Publisher entity) : base(entity)
        { }

        public string Name { get; set; }

        public IEnumerable<ViewBookModel> MyBooks { get; set; }
        public IEnumerable<ViewBookModel> ReferenceBooks { get; set; }
        public IEnumerable<ViewStoryModel> Stories { get; set; } 
    }
}