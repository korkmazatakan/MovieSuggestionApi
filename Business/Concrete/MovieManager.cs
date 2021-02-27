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
        private IInMovieImageDal _inImageDal;

        public MovieManager(IMovieDal movieDal,IDirectorService directorService)
        {
            _movieDal = movieDal;
            _directorService = directorService;
        }
        //[PerformanceAspect(2)]
        //[SecuredOperation("Movie.List")]
        [CacheAspect(5)]
        [LogAspect()]
        public IDataResult<List<Movie>> GetAll() => new SuccessDataResult<List<Movie>>(_movieDal.GetList().ToList());

        public IDataResult<Movie> GetById(int Id)
        {
            return new SuccessDataResult<Movie>(_movieDal.Get(m => m.Id == Id));
        }

        [LogAspect()]
        public IDataResult<List<Movie>> GetListByGenre(int GenreId)
        {
            return new SuccessDataResult<List<Movie>>(_movieDal.GetList(m => m.GenreId == GenreId).ToList());
        }

        [LogAspect()]
        public IDataResult<List<Movie>> GetListByDirector(int DirectorId)
        {
            return new SuccessDataResult<List<Movie>>(_movieDal.GetList(m => m.DirectorId == DirectorId).ToList());
        }

        [ValidationAspect(typeof(MovieValidator), Priority = 1)]
        [CacheRemoveAspect("IMovieService.Get")]
        [SecuredOperation("Movie.Add")]
        [LogAspect()]
        /*[CacheRemoveAspect("IGenreService.Get")] You can use this aspect attribute how many times you want */
        public IResult Add(MovieAddDto movie)
        {
            
            IResult result = BusinessRules.Run(CheckMovieExist(movie.Movie));
            if (result != null)
            {
                return result;
            }
            _movieDal.Add(movie.Movie);
            //movie id is taken
            
            InMovieImage img = new InMovieImage();
            img.ImageName = movie.images.FileName;
            img.MovieId = movie.Movie.Id;
            _inImageDal.Add(img);
        
            return new SuccessResult(Messages.MovieAdded);
        }

        [LogAspect()]
        public IResult Delete(Movie movie)
        {
            _movieDal.Delete(movie);
            return new SuccessResult(Messages.MovieDeleted);
        }

        [LogAspect()]
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

        private IResult CheckMovieNameExist(string movieName)
        {
            if (_movieDal.Get(mv =>mv.Name == movieName) != null)
            {
                return new ErrorResult(Messages.MovieNameAlreadyExisted);
            }

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

    }
}
