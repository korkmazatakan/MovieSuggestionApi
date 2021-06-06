using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ILanguageService
    {
        IDataResult<Language> GetById(int id);
        IDataResult<List<Language>> GetAll();
        IResult Add(Language language);
        IResult Delete(Language language);
        IResult Update(Language language);
    }
}