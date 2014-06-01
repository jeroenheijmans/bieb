using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Books
{
    public class LinkableBookModel : LinkableRootEntityModel<Book>
    {
        public LinkableBookModel(Book book)
        {
            Id = book.Id;
            Text = book.Title;
        }
    }

    public static class LinkableBookModelExtensions
    {
        public static LinkableBookModel AsLinkableBookModel(this Book book)
        {
            return book == null
                       ? null
                       : new LinkableBookModel(book);
        }
    }
}