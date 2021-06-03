using System;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class DirectorUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornAt { get; set; }
        public string BornIn { get; set; }
        public string Description { get; set; }
        public IFormFile Portre { get; set; }
    }
}