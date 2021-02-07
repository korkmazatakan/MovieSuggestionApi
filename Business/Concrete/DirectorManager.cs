using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Constants.Messages;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class DirectorManager : IDirectorService
    {
        private IDirectorDal _directorDal;

        public DirectorManager(IDirectorDal directorDal)
        {
            _directorDal = directorDal;
        }

        public IResult Add(Director director)
        {
            _directorDal.Add(director);
            return new SuccessResult(Messages.DirectorAdded);
        }

        public IResult Delete(Director director)
        {
            _directorDal.Delete(director);
            return new SuccessResult(Messages.DirectorDeleted);
        }

        public IDataResult<List<Director>> GetAll()
        {
            return new SuccessDataResult<List<Director>>(_directorDal.GetList().ToList());
        }

        public IDataResult<Director> GetById(int Id)
        {
            return new SuccessDataResult<Director>(_directorDal.Get(a => a.Id == Id));
        }

        public IResult Update(Director director)
        {
            _directorDal.Update(director);
            return new SuccessResult(Messages.DirectorUpdated);
        }
    }
}
