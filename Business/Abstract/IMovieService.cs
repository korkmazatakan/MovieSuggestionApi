using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IMovieService
    {
        IDataResult<List<Movie>> GetAll();
        IDataResult<List<Movie>> GetRandom(int number);
        IDataResult<Movie> GetById(int id);
        IDataResult<List<Movie>> GetListByGenre(int genre_id);
        IDataResult<List<Movie>> GetListByDirector(int director_id);
        IDataResult<IList<Movie>> GetByCount(int count);
        IDataResult<IList<Movie>> GetLastReleased(int count);
        IDataResult<IList<Movie>> GetTopBoxOffice(int count);
        IResult Add(Movie movie);
        IResult Delete(Movie movie);
        IResult Update(Movie movie);
        IResult TransactionTest(Movie movie);
        IDataResult<IList<Movie>> GetByQuery(string searchQuery);
        
    }
}
