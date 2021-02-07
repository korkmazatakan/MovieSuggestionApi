using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IGenreService
    {
        IDataResult<Genre> GetById(int id);
        IDataResult<List<Genre>> GetAll();
        IResult Add(Genre genre);
        IResult Delete(Genre genre);
        IResult Update(Genre genre);
    }
}
