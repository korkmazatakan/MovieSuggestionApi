using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class InMovieImageDto : IDto
    {
        public int movieId { get; set; }
        public IList<IFormFile> files { get; set; }

    }
}
