using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.Publishers
{
    public class LinkablePublisherModel : LinkableEntityModel<Publisher>
    {
        public LinkablePublisherModel(Publisher publisher)
        {
            Id = publisher.Id;
            Text = publisher.Name;
        }
    }

    public static class LinkablePublisherModelExtensions
    {
        public static LinkablePublisherModel AsLinkablePublisherModel(this Publisher publisher)
        {
            return publisher == null
                       ? null
                       : new LinkablePublisherModel(publisher);
        }
    }
}