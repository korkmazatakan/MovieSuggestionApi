using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
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

        [CacheAspect()]
        public IDataResult<List<Genre>> GetAll()
        {
            return new SuccessDataResult<List<Genre>>(_genreDal.GetList().ToList());
        }
        [CacheRemoveAspect("IGenreService.Get")]
        [SecuredOperation("Genre.Add")]
        [LogAspect()]
        public IResult Add(Genre genre)
        {
            _genreDal.Add(genre);
            return new SuccessResult(Messages.GenreAdded);
        }
        [CacheRemoveAspect("IGenreService.Get")]
        [SecuredOperation("Genre.Delete")]
        [LogAspect()]
        public IResult Delete(Genre genre)
        {
            _genreDal.Delete(genre);
            return new SuccessResult(Messages.GenreDeleted);
        }
        [CacheRemoveAspect("IGenreService.Get")]
        [SecuredOperation("Genre.Update")]
        [LogAspect()]
        public IResult Update(Genre genre)
        {
            _genreDal.Update(genre);
            return new SuccessResult(Messages.GenreUpdated);

        }
    }
}
