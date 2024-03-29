﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMovieDal:IEntityRepository<Movie>
    {
        public List<Movie> GetRandomMovie(int number);
    }
}
