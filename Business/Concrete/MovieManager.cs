using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MovieManager:IMovieService
    {
        private IMovieDal _movieDal;

        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }
        [PerformanceAspect(2)]
        //[SecuredOperation("Movie.List")]
        [CacheAspect(1)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Movie>> GetAll() => new SuccessDataResult<List<Movie>>(_movieDal.GetList().ToList());

        public IDataResult<Movie> GetById(int Id)
        {
            return new SuccessDataResult<Movie>(_movieDal.Get(m => m.Id == Id));
        }

        public IDataResult<List<Movie>> GetListByGenre(int GenreId)
        {
            return new SuccessDataResult<List<Movie>>(_movieDal.GetList(m => m.GenreId == GenreId).ToList());
        }

        public IDataResult<List<Movie>> GetListByDirector(int DirectorId)
        {
            return new SuccessDataResult<List<Movie>>(_movieDal.GetList(m => m.DirectorId == DirectorId).ToList());
        }

        //[ValidationAspect(typeof(MovieValidator), Priority = 1)]
        //[CacheRemoveAspect("IMovieService.Get")]
        [LogAspect(typeof(FileLogger))]
        /*[CacheRemoveAspect("IGenreService.Get")] You can use this aspect attribute how many times you want */
        public IResult Add(Movie movie)
        {
            _movieDal.Add(movie);
            return new SuccessResult(Messages.MovieAdded);
        }

        public IResult Delete(Movie movie)
        {
            _movieDal.Delete(movie);
            return new SuccessResult(Messages.MovieDeleted);
        }

        public IResult Update(Movie movie)
        {
            _movieDal.Update(movie);
            return new SuccessResult(Messages.MovieUpdated);
        }

        [TransactionScopeAspect]
        public IResult TransactionTest(Movie movie)
        {
            _movieDal.Update(movie);
            _movieDal.Delete(movie);
            

            return new SuccessResult();
        }
    }
}
