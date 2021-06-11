using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMovieDal:EfEntityRepositoryBase<Movie,MovieSuggestionContext>, IMovieDal
    {
        public List<Movie> GetRandomMovie(int number)
        {
            string sqlcommand = $"SELECT TOP {number} * FROM Movies ORDER BY NEWID()";
            
            using (var context = new MovieSuggestionContext())
            {
                var movies = context.Movies.FromSqlRaw(sqlcommand);
                return movies.ToList();
            }
        }
    }
}
