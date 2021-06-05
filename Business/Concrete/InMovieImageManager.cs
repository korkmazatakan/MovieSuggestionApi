using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class InMovieImageManager:IInMovieImageService
    {
        private IInMovieImageDal _imageDal;

        public InMovieImageManager(IInMovieImageDal imageDal)
        {
            _imageDal = imageDal;
        }

        public IDataResult<InMovieImage> GetById(string id)
        {
            return new DataResult<InMovieImage>(_imageDal.Get(img => img.Id.Equals(id)), true);
        }

        public IDataResult<IList<InMovieImage>> GetByMovieId(int movieId)
        {
            return new DataResult<IList<InMovieImage>>(_imageDal.GetList(img => img.MovieId == movieId),true);
        }

        public IResult Add(InMovieImage image)
        {
            _imageDal.Add(image);
            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult Delete(InMovieImage image)
        {
            _imageDal.Delete(image);
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IResult Update(InMovieImage image)
        {
            _imageDal.Update(image);
            return new SuccessResult(Messages.ImageUpdated);
        }
    }
}
