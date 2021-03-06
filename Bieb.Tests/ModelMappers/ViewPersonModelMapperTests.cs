﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.CustomDataTypes;
using Bieb.Domain.Entities;
using Bieb.Web.Localization;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Stories;
using Moq;
using NUnit.Framework;

namespace Bieb.Tests.ModelMappers
{
    [TestFixture]
    public class ViewPersonModelMapperTests
    {
        private ViewPersonModelMapper mapper;

        [SetUp]
        public void SetUp()
        {
            var iso639LanguageDisplayer = new Mock<IIso639LanguageDisplayer>().Object;
            var bookMapper = new ViewBookModelMapper(iso639LanguageDisplayer);
            var storyMapper = new ViewStoryModelMapper(iso639LanguageDisplayer);
            mapper = new ViewPersonModelMapper(bookMapper, storyMapper);
        }


        [Test]
        public void Can_Set_IsGenderKnown_Based_On_Gender_Enum()
        {
            foreach (var gender in (Gender[]) Enum.GetValues(typeof (Gender)))
            {
                var model = mapper.ModelFromEntity(new Person {Gender = gender});
                Assert.That((gender == Gender.Unknown && !model.IsGenderKnown) || model.IsGenderKnown, "Should have correct IsGenderKnown for {0}", gender);
            }
        }


        [Test]
        public void Will_Set_Nondefault_String_For_Gender()
        {
            foreach (var gender in (Gender[])Enum.GetValues(typeof(Gender)))
            {
                var model = mapper.ModelFromEntity(new Person { Gender = gender });
                Assert.That(model.Gender != default(string), "Gender '{0}' should have non-default char to represent it.", gender);
            }
        }


        [Test]
        public void Has_Different_NonEmpty_Tooltip_For_Each_Gender()
        {
            var models = ((Gender[])Enum.GetValues(typeof(Gender))).Select(g => mapper.ModelFromEntity(new Person { Gender = g }));
            Assert.That(models.Count(), Is.EqualTo(models.Select(m => m.GenderTooltip).Distinct().Count()));
        }


        [Test]
        public void Will_Have_Known_Nationality_For_Non_Empty_Nationality()
        {
            var person = new Person() {Nationality = "NL"};
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.IsNationalityKnown);
        }


        [Test]
        public void Can_Create_Comma_Separated_Tags_String()
        {
            var person = new Person();
            var book = new Book();
            book.AddTag(new Tag("SF"));
            book.AddTag(new Tag("Fantasy"));
            book.AddAuthor(person);

            var model = mapper.ModelFromEntity(person);

            Assert.That(model.Tags, Is.EqualTo("SF, Fantasy"));
        }


        [Test]
        public void Will_Know_If_There_Are_Tags()
        {
            var person = new Person();
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.HasTags, Is.False);
        }


        [Test]
        public void Will_Know_If_There_Are_No_Tags()
        {
            var person = new Person();
            var model = mapper.ModelFromEntity(person);
            var book = new Book();
            book.AddTag(new Tag("SF"));
            book.AddAuthor(person);

            Assert.That(model.HasTags, Is.False);
        }


        [Test]
        public void Can_Create_Comma_Separated_Roles_String()
        {
            // This test seems fragile. When it breaks it probably means proper localization
            // for this model property is needed (or in progress :D). Until then it's better
            // than nothing.

            var person = new Person();
            var book1 = new Book();
            var book2 = new Book();

            book1.AddAuthor(person);
            book2.AddEditor(person);

            var model = mapper.ModelFromEntity(person);

            Assert.That(model.Roles, Is.EqualTo("Author, Editor"));
        }


        [Test]
        public void Place_Of_Birth_Null_Is_Considered_Unknown_Place()
        {
            var person = new Person { PlaceOfBirth = null };
            var model = mapper.ModelFromEntity(person);
            Assert.That(!model.IsPlaceOfBirthKnown);
        }


        [Test]
        public void Place_Of_Birth_Empty_Is_Considered_Unknown_Place()
        {
            var person = new Person { PlaceOfBirth = "" };
            var model = mapper.ModelFromEntity(person);
            Assert.That(!model.IsPlaceOfBirthKnown);
        }


        [Test]
        public void Place_Of_Birth_Non_Empty_Is_Considered_Unknown_Place()
        {
            var person = new Person { PlaceOfBirth = "x" };
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.IsPlaceOfBirthKnown);
        }


        [Test]
        public void Place_Of_Death_Null_Is_Considered_Unknown_Place()
        {
            var person = new Person { PlaceOfDeath = null };
            var model = mapper.ModelFromEntity(person);
            Assert.That(!model.IsPlaceOfDeathKnown);
        }


        [Test]
        public void Place_Of_Death_Empty_Is_Considered_Unknown_Place()
        {
            var person = new Person { PlaceOfDeath = "" };
            var model = mapper.ModelFromEntity(person);
            Assert.That(!model.IsPlaceOfDeathKnown);
        }


        [Test]
        public void Place_Of_Death_Non_Empty_Is_Considered_Unknown_Place()
        {
            var person = new Person { PlaceOfDeath = "x" };
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.IsPlaceOfDeathKnown);
        }


        [Test]
        public void Will_Have_Same_Number_Of_AuthoredBooks_As_Source_Entity()
        {
            var person = new Person();
            var book = new Book();
            book.AddAuthor(person);
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.AuthoredBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Have_Same_Number_Of_EditedBooks_As_Source_Entity()
        {
            var person = new Person();
            var book = new Book();
            book.AddEditor(person);
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.EditedBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Have_Same_Number_Of_TranslatedBook_As_Source_Entity()
        {
            var person = new Person();
            var book = new Book();
            book.AddTranslator(person);
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.TranslatedBooks.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Have_Same_Number_Of_TranslatedStories_As_Source_Entity()
        {
            var person = new Person();
            var story = new Story();
            story.AddTranslator(person);
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.TranslatedStories.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Have_Same_Number_Of_AuthoredStories_As_Source_Entity()
        {
            var person = new Person();
            var story = new Story();
            story.AddAuthor(person);
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.AuthoredStories.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Will_Set_IsDateOfBirthKnown_If_Known()
        {
            var person = new Person { DateOfBirth = new UncertainDate(1990) };
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.IsDateOfBirthKnown, Is.True);
        }


        [Test]
        public void Will_Set_IsDateOfDeathKnown_If_Known()
        {
            var person = new Person { DateOfDeath = new UncertainDate(1990) };
            var model = mapper.ModelFromEntity(person);
            Assert.That(model.IsDateOfDeathKnown, Is.True);
        }
    }
}
