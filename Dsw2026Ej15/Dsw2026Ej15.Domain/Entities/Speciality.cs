using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public Speciality(string name, string description, Guid id)
        {
            Name = name;
            Description = description;
            Id = id;
        }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
