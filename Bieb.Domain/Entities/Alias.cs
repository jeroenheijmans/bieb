using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    class Alias : BaseEntity
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Prefix { get; set; }
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return (
                        (string.IsNullOrWhiteSpace(Title) ? "" : Title + " ")
                        +
                        (string.IsNullOrWhiteSpace(FirstName) ? "" : FirstName + " ")
                        +
                        (string.IsNullOrWhiteSpace(Prefix) ? "" : Prefix + " ")
                        +
                        (string.IsNullOrWhiteSpace(Surname) ? "" : Surname)
                    ).Trim();
            }
        }
    }
}
