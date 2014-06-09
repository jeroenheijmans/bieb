using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Domain.Repositories;

namespace Bieb.Web.Models.People
{
    public class EditPersonReviewModelMapper : EditEntityModelMapper<Review<Person>, EditPersonReviewModel>
    {
        private readonly IEntityRepository<Person> people;

        public EditPersonReviewModelMapper(IEntityRepository<Person> people)
        {
            if (people == null)
            {
                throw new ArgumentNullException("people");
            }

            this.people = people;
        }

        public override void MergeEntityWithModel(Review<Person> entity, EditPersonReviewModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Rating = model.Rating;
            entity.ReviewText = model.Text;

            var book = people.GetItem(model.PersonId);

            if (book == null)
            {
                throw new MappingException("Provided PersonId for review wasn't for any known person.");
            }

            entity.Subject = book;
        }

        public override EditPersonReviewModel ModelFromEntity(Review<Person> entity)
        {
            var model = base.ModelFromEntity(entity);

            model.PersonId = entity.Subject.Id;
            model.Text = entity.ReviewText;
            model.Rating = entity.Rating;

            return model;
        }
    }
}