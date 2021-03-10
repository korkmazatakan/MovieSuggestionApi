using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Movie:IEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public string Poster { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
