using System;
using System.Collections.Generic;
using System.Linq;
using Bieb.Domain.Entities;

namespace Bieb.Web.Models.People
{
    public class LinkablePersonModel : LinkableEntityModel<Person>
    {
        public LinkablePersonModel(Person person)
        {
            Id = person.Id;
            Text = person.FullName;
        }
    }

    public static class LinkablePersonModelExtensions
    {
        public static LinkablePersonModel AsLinkablePersonModel(this Person person)
        {
            return person == null
                       ? null
                       : new LinkablePersonModel(person);
        }
    }
}