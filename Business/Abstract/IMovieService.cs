using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMovieService
    {
        IDataResult<List<Movie>> GetAll();
        IDataResult<Movie> GetById(int id);
        IDataResult<List<Movie>> GetListByGenre(int genre_id);
        IDataResult<List<Movie>> GetListByDirector(int director_id);
        IResult Add(Movie movie);
        IResult Delete(Movie movie);
        IResult Update(Movie movie);
        IResult TransactionTest(Movie movie);
    }
}
