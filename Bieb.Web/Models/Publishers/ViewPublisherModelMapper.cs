using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;
using Bieb.Web.Models.Books;
using Bieb.Web.Models.Stories;

namespace Bieb.Web.Models.Publishers
{
    public class ViewPublisherModelMapper : IViewEntityModelMapper<Publisher, ViewPublisherModel>
    {
        private readonly IViewEntityModelMapper<Book, ViewBookModel> bookMapper;
        private readonly IViewEntityModelMapper<Story, ViewStoryModel> storyMapper;

        public ViewPublisherModelMapper(IViewEntityModelMapper<Book, ViewBookModel> bookMapper, IViewEntityModelMapper<Story, ViewStoryModel> storyMapper)
        {
            this.bookMapper = bookMapper;
            this.storyMapper = storyMapper;
        }

        public ViewPublisherModel ModelFromEntity(Publisher entity)
        {
            return new ViewPublisherModel(entity)
                       {
                           Name = entity.Name,
                           MyBooks = entity.MyBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           ReferenceBooks = entity.ReferenceBooks.Select(b => bookMapper.ModelFromEntity(b)),
                           Stories = entity.Stories.Select(s => storyMapper.ModelFromEntity(s))
                       };
        }
    }
}