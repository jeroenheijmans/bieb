﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieb.Domain.Entities
{
    public class Review<T> : BaseEntity where T : IReviewable
    {
        public virtual string ReviewText { get; set; }
        public virtual int Rating { get; set; }

        public virtual T Subject { get; set; }
    }
}