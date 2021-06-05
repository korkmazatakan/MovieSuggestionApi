using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IDirectorService 
    {
        IDataResult<Director> GetById(int id);
        IDataResult<IList<Director>> GetAll();
        IDataResult<IList<Director>> GetByQuery(string searchQuery);
        IResult Add(Director director);
        IResult Delete(Director director);
        IResult Update(Director director);
    }
}
