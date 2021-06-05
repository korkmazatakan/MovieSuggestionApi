using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DirectorAddDto:IDto
    {
        public string Name { get; set; }
        public DateTime BornAt { get; set; }
        public string BornIn { get; set; }
        public string Description { get; set; }
        public IFormFile Portre { get; set; }
    }
}
