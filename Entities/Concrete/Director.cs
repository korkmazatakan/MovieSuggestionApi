using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Director : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornAt { get; set; }
        public string BornIn { get; set; }
        public string Description { get; set; }
        public string Portre { get; set; }

    }
}
