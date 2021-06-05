using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Constants.Messages;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspect.Autofac;

namespace Business.Concrete
{
    public class DirectorManager : IDirectorService
    {
        private IDirectorDal _directorDal;

        public DirectorManager(IDirectorDal directorDal)
        {
            _directorDal = directorDal;
        }

        [CacheRemoveAspect("IDirectorService.Get")]
        [SecuredOperation("Director.Add")]
        [LogAspect()]
        public IResult Add(Director director)
        {
            _directorDal.Add(director);
            return new SuccessResult(Messages.DirectorAdded);
        }
        [CacheRemoveAspect("IDirectorService.Get")]
        [SecuredOperation("Director.Delete")]
        [LogAspect()]
        public IResult Delete(Director director)
        {
            _directorDal.Delete(director);
            return new SuccessResult(Messages.DirectorDeleted);
        }
        
        [CacheAspect()]
        public IDataResult<IList<Director>> GetAll()
        {
            return new SuccessDataResult<IList<Director>>(_directorDal.GetList().ToList());
        }

        public IDataResult<Director> GetById(int Id)
        {
            return new SuccessDataResult<Director>(_directorDal.Get(a => a.Id == Id));
        }

        public IDataResult<IList<Director>> GetByQuery(string searchQuery)
        {
            IList<Director> directors = null;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                directors = _directorDal.GetList(x => x.Name.Contains(searchQuery)).ToList();
            }
            return new SuccessDataResult<IList<Director>>(directors);
        }

        [CacheRemoveAspect("IDirectorService.Get")]
        [SecuredOperation("Director.Update")]
        [LogAspect()]
        public IResult Update(Director director)
        {
            _directorDal.Update(director);
            return new SuccessResult(Messages.DirectorUpdated);
        }

    }
}
