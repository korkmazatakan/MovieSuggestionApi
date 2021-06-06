using System;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class MovieUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public int LanguageId { get; set; }
        public  long BoxOffice { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile PosterFile { get; set; }
    }
}