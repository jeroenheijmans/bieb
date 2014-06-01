using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bieb.Domain.Entities;
using Bieb.Web.Models;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.People;
using Bieb.Web.Models.Publishers;
using Bieb.Web.Models.Series;
using Bieb.Web.Models.Stories;
using Ninject.Modules;

namespace Bieb.Web.Infrastructure.NinjectModules
{
    public class ModelMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<EditEntityModelMapper<Book, EditBookModel>>().To<EditBookModelMapper>();
            Bind<EditEntityModelMapper<Review<Book>, EditBookReviewModel>>().To<EditBookReviewModelMapper>();
            Bind<EditEntityModelMapper<Person, EditPersonModel>>().To<EditPersonModelMapper>();
            Bind<EditEntityModelMapper<Review<Person>, EditPersonReviewModel>>().To<EditPersonReviewModelMapper>();
            Bind<EditEntityModelMapper<Publisher, EditPublisherModel>>().To<EditPublisherModelMapper>();
            Bind<EditEntityModelMapper<Series, EditSeriesModel>>().To<EditSeriesModelMapper>();
            Bind<EditEntityModelMapper<Story, EditStoryModel>>().To<EditStoryModelMapper>();
        }
    }
}