using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class GenreManager:IGenreService
    {
        private IGenreDal _genreDal;

        public GenreManager(IGenreDal genreDal)
        {
            _genreDal = genreDal;
        }

        public IDataResult<Genre> GetById(int id)
        {
            return new SuccessDataResult<Genre>(_genreDal.Get(genre => genre.Id == id));
        }

        
        public IDataResult<List<Genre>> GetAll()
        {
            return new SuccessDataResult<List<Genre>>(_genreDal.GetList().ToList());
        }

        public IResult Add(Genre genre)
        {
            _genreDal.Add(genre);
            return new SuccessResult(Messages.GenreAdded);
        }

        public IResult Delete(Genre genre)
        {
            _genreDal.Delete(genre);
            return new SuccessResult(Messages.GenreDeleted);
        }

        public IResult Update(Genre genre)
        {
            _genreDal.Update(genre);
            return new SuccessResult(Messages.GenreUpdated);

        }
    }
}
