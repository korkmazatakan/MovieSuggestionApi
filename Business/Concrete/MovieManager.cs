﻿using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class MovieManager:IMovieService
    {
        private IMovieDal _movieDal;
        private IDirectorService _directorService;
        private IInMovieImageService _inImageDal;

        public MovieManager(IMovieDal movieDal,IDirectorService directorService, IInMovieImageService inImageDal)
        {
            _movieDal = movieDal;
            _directorService = directorService;
            _inImageDal = inImageDal;
        }
        //[PerformanceAspect(2)]
        //[SecuredOperation("Movie.List")]
        [CacheAspect()]
        public IDataResult<List<Movie>> GetAll() => new SuccessDataResult<List<Movie>>(_movieDal.GetList().ToList());
        
        [CacheAspect()]
        public IDataResult<List<Movie>> GetRandom(int number)
        {
            return new SuccessDataResult<List<Movie>>(_movieDal.GetRandomMovie(number));
        }

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

        public IDataResult<IList<Movie>> GetByCount(int count)
        {
            List<Movie> listOfMovie = _movieDal.GetList().ToList();
            listOfMovie.Sort((x,y) =>DateTime.Compare(y.ReleaseDate,x.ReleaseDate));
            return new SuccessDataResult<IList<Movie>>(
                listOfMovie.Take(count).ToList());
        }

        public IDataResult<IList<Movie>> GetLastReleased(int count)
        {
            List<Movie> listOfMovie = _movieDal.GetList().ToList();
            listOfMovie.Sort((x, y) => DateTime.Compare(y.ReleaseDate , x.ReleaseDate));

            return new SuccessDataResult<IList<Movie>>(listOfMovie.Take(count).ToList());
        }

        [ValidationAspect(typeof(MovieValidator), Priority = 1)]
        [CacheRemoveAspect("IMovieService.Get")]
        [SecuredOperation("Movie.Add")]
        [LogAspect()]
        /*[CacheRemoveAspect("IGenreService.Get")]*/ /* You can use this aspect attribute how many times you want */
        public IResult Add(Movie movie)
      {     
            IResult result = BusinessRules.Run(CheckMovieExist(movie));
            if (result != null)
            {
                return result;
            }
            _movieDal.Add(movie);
            //movie id is taken
            return new SuccessResult(Messages.MovieAdded);
        }

        [SecuredOperation("Movie.Delete")]
        [CacheRemoveAspect("IMovieService.Get")]
        [LogAspect()]
        public IResult Delete(Movie movie)
        {
            _movieDal.Delete(movie);
            return new SuccessResult(Messages.MovieDeleted);
        }

        [SecuredOperation("Movie.Update")]
        [CacheRemoveAspect("IMovieService.Get")]
        [LogAspect()]
        public IResult Update(Movie movie)
        {
            _movieDal.Update(movie);
            return new SuccessResult(Messages.MovieUpdated);
        }

        [SecuredOperation("Movie.Delete")]
        [CacheRemoveAspect("IMovieService.Get")]
        [LogAspect()]
        [TransactionScopeAspect]
        public IResult TransactionTest(Movie movie)
        {
            _movieDal.Update(movie);
            _movieDal.Delete(movie);
            

            return new SuccessResult();
        }

        private IResult CheckMovieExist(Movie movie)
        {
            if (_movieDal.Get(mv => mv.Name == movie.Name && mv.DirectorId == movie.DirectorId) != null)
            {
                return new ErrorResult(Messages.MovieAlreadyExisted);
            }

            return new SuccessResult();
        }

        public IDataResult<IList<Movie>> GetTopBoxOffice(int count)
        {
            List<Movie> listOfMovie = _movieDal.GetList().ToList();
            listOfMovie.Sort((x, y) => y.BoxOffice.CompareTo(x.BoxOffice)) ;

            return new SuccessDataResult<IList<Movie>>(listOfMovie.Take(count).ToList());
        }

        public IDataResult<IList<Movie>> GetByQuery(string searchQuery)
        {
            List<Movie> movies = null;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                movies = _movieDal.GetList(x => x.Name.Contains(searchQuery)).ToList();
            }
            return new SuccessDataResult<IList<Movie>>(movies);
        }
    }
}
