using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Genre:IEntity
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}
