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
        IDataResult<List<Director>> GetAll();
        IResult Add(Director director);
        IResult Delete(Director director);
        IResult Update(Director director);
    }
}
