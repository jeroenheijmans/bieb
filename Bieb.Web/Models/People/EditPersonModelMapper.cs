using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.CustomDataTypes;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class EditPersonModelMapper : EditEntityModelMapper<Person, EditPersonModel>
    {
        public override void MergeEntityWithModel(Person entity, EditPersonModel model)
        {
            base.MergeEntityWithModel(entity, model);

            entity.Title = model.Title;
            entity.FirstName = model.FirstName;
            entity.Prefix = model.Prefix;
            entity.Surname = model.Surname;

            entity.Gender = model.Gender;
            entity.Nationality = model.Nationality;

            entity.PlaceOfBirth = model.PlaceOfBirth;
            entity.PlaceOfDeath = model.PlaceOfDeath;

            entity.DateOfBirth = new UncertainDate(model.BirthYear, model.BirthMonth, model.BirthDay);
            entity.DateOfDeath = new UncertainDate(model.DeathYear, model.DeathMonth, model.DeathDay);
        }

        public override EditPersonModel ModelFromEntity(Person entity)
        {
            var model = base.ModelFromEntity(entity);

            model.Title = entity.Title;
            model.FirstName = entity.FirstName;
            model.Prefix = entity.Prefix;
            model.Surname = entity.Surname;

            model.Gender = entity.Gender;
            model.Nationality = entity.Nationality;

            model.PlaceOfBirth = entity.PlaceOfBirth;
            model.PlaceOfDeath = entity.PlaceOfDeath;

            model.BirthDay = entity.DateOfBirth.Day;
            model.BirthMonth = entity.DateOfBirth.Month;
            model.BirthYear = entity.DateOfBirth.Year;

            model.DeathDay = entity.DateOfDeath.Day;
            model.DeathMonth = entity.DateOfDeath.Month;
            model.DeathYear = entity.DateOfDeath.Year;

            model.FullName = entity.FullName;

            return model;
        }
    }
}