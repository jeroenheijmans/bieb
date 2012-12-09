using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class SurpriseController : Controller
    {
        private IEntityRepository<Person> PersonRepository { get; set; }
        private IEntityRepository<Book> BookRepository { get; set; }

        public SurpriseController(IEntityRepository<Person> PersonRepository, IEntityRepository<Book> BookRepository)
        {
            this.PersonRepository = PersonRepository;
            this.BookRepository = BookRepository;
        }

        public ActionResult Index()
        {
            if (!PersonRepository.Items.Any() || !BookRepository.Items.Any())
                return RedirectToAction("EmptyDatabase", "Home");

            if (new Random().Next(2) > 0)
            {
                var person = PersonRepository.GetRandomItem();
                Debug.Assert(person != null, "Expected to find at least one random person, but found none.");
                return RedirectToAction("Details", "People", new { id = person.Id });
            }
            else
            {
                var book = BookRepository.GetRandomItem();
                Debug.Assert(book != null, "Expected to find at least one random book, but found none.");
                return RedirectToAction("Details", "Books", new { id = book.Id });
            }
        }
    }
}
