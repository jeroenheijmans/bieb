using System;
using System.Collections.Generic;
using System.Linq;
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
            Bind<IViewEntityModelMapper<Book, ViewBookModel>>().To<ViewBookModelMapper>();
            Bind<IViewEntityModelMapper<Person, ViewPersonModel>>().To<ViewPersonModelMapper>();
            Bind<IViewEntityModelMapper<Publisher, ViewPublisherModel>>().To<ViewPublisherModelMapper>();
            Bind<IViewEntityModelMapper<Series, ViewSeriesModel>>().To<ViewSeriesModelMapper>();
            Bind<IViewEntityModelMapper<Story, ViewStoryModel>>().To<ViewStoryModelMapper>();

            Bind<IEditEntityModelMapper<Book, EditBookModel>>().To<EditBookModelMapper>();
            Bind<IEditEntityModelMapper<Person, EditPersonModel>>().To<EditPersonModelMapper>();
            Bind<IEditEntityModelMapper<Publisher, EditPublisherModel>>().To<EditPublisherModelMapper>();
            Bind<IEditEntityModelMapper<Series, EditSeriesModel>>().To<EditSeriesModelMapper>();
            Bind<IEditEntityModelMapper<Story, EditStoryModel>>().To<EditStoryModelMapper>();
        }
    }
}