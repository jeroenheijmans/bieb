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
    }
}
