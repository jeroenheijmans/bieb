using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Stories
{
    public class EditStoryModel : EditPublishableModel<Story>
    {
        public EditStoryModel()
        { }

        public EditStoryModel(string bookTitle)
        {
            this.BookTitle = bookTitle;
        }

        public string BookTitle { get; internal set; }
    }
}