using System;
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
                return RedirectToAction("Details", "People", new { id = person.Id });
            }
            else
            {
                var book = BookRepository.GetRandomItem();
                return RedirectToAction("Details", "Books", new { id = book.Id });
            }
        }
    }
}
