using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Books;

namespace Bieb.Web.Models.Series
{
    public class ViewSeriesModelMapper : IViewEntityModelMapper<Domain.Entities.Series, ViewSeriesModel>
    {
        private readonly IViewEntityModelMapper<Book, ViewBookModel> bookMapper;

        public ViewSeriesModelMapper(IViewEntityModelMapper<Book, ViewBookModel> bookMapper)
        {
            this.bookMapper = bookMapper;
        }

        public ViewSeriesModel ModelFromEntity(Domain.Entities.Series entity)
        {
            return new ViewSeriesModel(entity)
                       {
                           Title = entity.Title,
                           Subtitle = entity.Subtitle,
                           Books = entity.Books.Values.Select(b => bookMapper.ModelFromEntity(b))
                       };
        }
    }
}