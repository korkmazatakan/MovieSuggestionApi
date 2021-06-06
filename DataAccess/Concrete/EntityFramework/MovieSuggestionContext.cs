using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class MovieSuggestionContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:otokon.database.windows.net,1433;Initial Catalog=FilmOneri;Persist Security Info=False;User ID=otokon58;Password=selami58A;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
             //optionsBuilder.UseSqlServer(@"Server=localhost,1433;Initial Catalog=FilmOneri;Persist Security Info=False;User ID=sa;Password=change_this_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<InMovieImage> InMovieImages { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
