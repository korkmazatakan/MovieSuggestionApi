using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class MovieAddDto:IDto
    {
        public Movie Movie { get; set; }
        public IFormFile images { get; set; }
    }
}
