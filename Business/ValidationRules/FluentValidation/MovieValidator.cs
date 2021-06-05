using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MovieValidator:AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
            RuleFor(m => m.Poster).NotEmpty();
            RuleFor(m => m.ReleaseDate).NotEmpty();
            RuleFor(m => m.DirectorId).NotEmpty();
            RuleFor(m => m.GenreId).NotEmpty();
        }

    }
}
