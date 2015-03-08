using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using NUnit.Framework;

namespace Bieb.Tests.Domain
{
    [TestFixture]
    public class SeriesTests 
    {
        [Test]
        public void Default_Constructor_Will_Set_Title_To_Empty_String()
        {
            var series = new Series();
            Assert.That(series.Title, Is.EqualTo(""));
        }


        [Test]
        public void ToString_Will_Return_Name()
        {
            var entity = new Series() { Title = "Xyz" };
            Assert.That(entity.ToString(), Is.EqualTo(entity.Title));
        }


        [Test]
        public void ToString_Can_Handle_Null_Name()
        {
            var entity = new Series() { Title = null };
            Assert.That(entity.ToString(), Is.Not.Null.Or.Empty);
        }


        [Test]
        public void TitleSort_Will_Fall_Back_To_Title()
        {
            var series = new Series
                             {
                                 Title = "The Monster",
                                 TitleSort = null
                             };
            
            Assert.That(series.TitleSort, Is.EqualTo(series.Title));
        }


        [Test]
        public void Can_Add_Book()
        {
            var series = new Series();
            series.AddBook(new Book("The Load"));
            Assert.That(series.Books.Count(), Is.EqualTo(1));
        }


        [Test]
        public void Can_Add_Multiple_Books()
        {
            var series = new Series();
            series.AddBook(new Book("The Load"));
            series.AddBook(new Book("Far and Away"));
            Assert.That(series.Books.Count(), Is.EqualTo(2));
        }


        [Test]
        public void Add_Book_Will_Set_Series_On_Book()
        {
            var series = new Series();
            series.AddBook(new Book());
            Assert.That(series.Books.Single().Value.Series, Is.EqualTo(series));
        }
    }
}
