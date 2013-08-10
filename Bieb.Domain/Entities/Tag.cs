﻿namespace Bieb.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }


        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
