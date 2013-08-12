using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Controllers
{
    public class SurpriseController : Controller
    {
        private IEntityRepository<Person> PersonRepository { get; set; }
        private IEntityRepository<LibraryBook> BookRepository { get; set; }

        public SurpriseController(IEntityRepository<Person> PersonRepository, IEntityRepository<LibraryBook> BookRepository)
        {
            this.PersonRepository = PersonRepository;
            this.BookRepository = BookRepository;
        }

        public ActionResult Index()
        {
            return Index(new RandomTypePicker());
        }

        internal ActionResult Index(IRandomEntityPicker RandomTypeGenerator)
        {
            if (!PersonRepository.Items.Any() || !BookRepository.Items.Any())
                return RedirectToAction("EmptyDatabase", "Home");

            if (RandomTypeGenerator.getRandomEntityType() == typeof(Person))
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

        private class RandomTypePicker : IRandomEntityPicker
        {
            Random random = new Random();

            public Type getRandomEntityType()
            {
                if (random.Next(2) > 0)
                    return typeof(Person);
                else
                    return typeof(Book);
            }
        }
    }

    public interface IRandomEntityPicker
    {
        Type getRandomEntityType();
    }

}
