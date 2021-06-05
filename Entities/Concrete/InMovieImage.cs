using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class InMovieImage:IEntity
    {
        public string Id { get; set; }
        public int MovieId { get; set; }
        public string ImageName { get; set; }
    }
}
