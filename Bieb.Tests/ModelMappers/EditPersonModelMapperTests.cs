﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.CustomDataTypes;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using Bieb.Web.Models.People;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class EditPersonModelMapperTests
    {
        private IEditEntityModelMapper<Person, EditPersonModel> mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new EditPersonModelMapper();
        }


        [Test]
        public void Can_Create_Model_From_Entity()
        {
            // Smoke test, I suppose
            var person = new Person {Surname = "Doe"};
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.Surname, Is.EqualTo(person.Surname));
        }


        [Test]
        public void Can_Merge_DateOfBirth_Info_From_Model_To_Entity()
        {
            var model = new EditPersonModel { BirthYear = 1950, BirthMonth = 5, BirthDay = 20 };
            var person = new Person();
            mapper.MergeEntityWithModel(person, model);
            Assert.That(person.DateOfBirth, Is.EqualTo(new UncertainDate(1950, 5, 20)));
        }


        [Test]
        public void Can_Merge_DateOfDeath_Info_From_Model_To_Entity()
        {
            var model = new EditPersonModel { DeathYear = 1950, DeathMonth = 5, DeathDay = 20 };
            var person = new Person();
            mapper.MergeEntityWithModel(person, model);
            Assert.That(person.DateOfDeath, Is.EqualTo(new UncertainDate(1950, 5, 20)));
        }
    }
}
