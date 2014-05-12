using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Models.Series
{
    public class EditSeriesModelMapper : EditEntityModelMapper<Domain.Entities.Series, EditSeriesModel>
    {
        private IEnumerable<Book> books; 

        public EditSeriesModelMapper(IEntityRepository<Book> books)
        {
            this.books = books.Items;
        }

        public override void MergeEntityWithModel(Domain.Entities.Series entity, EditSeriesModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;

            var i = 0;

            entity.Books.Clear();
            
            foreach (var bookId in model.BookIds)
            {
                var book = books.FirstOrDefault(b => b.Id == bookId);
                
                if (book == null)
                {
                    throw new MappingException("Provided book Id could not be found in the list of available books.");
                }

                entity.Books.Add(i, book);

                i++;
            }
        }

        public override EditSeriesModel ModelFromEntity(Domain.Entities.Series entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Title = entity.Title;
            model.Subtitle = entity.Subtitle;

            model.AvailableBooks = new SelectList(books.OrderBy(b => b.Title), "Id", "TitleSort");

            model.BookIds = entity.Books.Select(book => book.Value.Id).ToArray();

            return model;
        }
    }
}