﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Search;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Controllers
{
    public class SearchController : Controller
    {
        protected IEntityRepository<Person> PersonRepository { get; set; }
        protected IEntityRepository<Book> BookRepository { get; set; }
        protected IEntityRepository<Story> StoryRepository { get; set; }

        // TODO: Make these dependencies more flexible. For now, as a workaround, the controller is dependent
        // on the three most basic repositories. With something like NHibernate Search the approach will be 
        // completely different anyways, but this set up will (have to) do for now.
        public SearchController(IEntityRepository<Person> PersonRepository, IEntityRepository<Book> BookRepository, IEntityRepository<Story> StoryRepository)
        {
            this.PersonRepository = PersonRepository;
            this.BookRepository = BookRepository;
            this.StoryRepository = StoryRepository;
        }

        public ActionResult Basic([Bind(Prefix="q")]string query)
        {
            if (query == null)
                return View(new BasicSearchResultModel
                {
                    Books = new LinkableBookModel[] { },
                    People = new LinkablePersonModel[] { },
                    Stories = new LinkableStoryModel[] { },
                    Query = null
                });

            var queryLowerCased = query.ToLower();

            // TODO: this now lists all matches, potentially EVERYTHING in the database. This may not be 
            // a disaster, given the number of expected items, but is certainly not very pretty. However,
            // given that this whole setup will probably be replaced with NHibernate Search, we'll leave
            // it like this for now.

            IEnumerable<Person> people = PersonRepository
                .Items
                .Where(p => p.FirstName.ToLower().Contains(queryLowerCased) || p.Surname.ToLower().Contains(queryLowerCased))
                .OrderBy(p => p.Surname)
                .Select(p => p);

            IEnumerable<Book> books = BookRepository
                .Items
                .Where(b => b.Title.ToLower().Contains(queryLowerCased))
                .OrderBy(b => b.Title)
                .Select(b => b);

            IEnumerable<Story> stories = StoryRepository
                .Items
                .Where(s => s.Title.ToLower().Contains(queryLowerCased))
                .OrderBy(s => s.Title)
                .Select(s => s);

            var model = new BasicSearchResultModel()
                            {
                                People = people.Select(p => p.AsLinkablePersonModel()),
                                Books = books.Select(b => b.AsLinkableBookModel()),
                                Stories = stories.Select(s => s.AsLinkableStoryModel()),
                                Query = query
                            };

            if (people.Count() == 1 && !books.Any() && !stories.Any())
            {
                return RedirectToAction("Details", "People", new { id = people.First().Id });
            }

            if (books.Count() == 1 && !people.Any() && !stories.Any())
            {
                return RedirectToAction("Details", "Books", new { id = books.First().Id });
            }

            if (stories.Count() == 1 && !people.Any() && !books.Any())
            {
                return RedirectToAction("Details", "Books", new { id = stories.First().Id });
            }

            return View(model);
        }
    }

}
