using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class LanguageManager:ILanguageService
    {
        private ILanguageDal _languageDal;

        public LanguageManager(ILanguageDal languageDal)
        {
            _languageDal = languageDal;
        }

        public IDataResult<Language> GetById(int id)
        {
            return new SuccessDataResult<Language>(_languageDal.Get(lang => lang.Id == id));
        }

        public IDataResult<List<Language>> GetAll()
        {
            return new SuccessDataResult<List<Language>>(_languageDal.GetList().ToList());
        }

        public IResult Add(Language language)
        {
            _languageDal.Add(language);
            return new SuccessResult(Messages.LanguageAdded);
        }

        public IResult Delete(Language language)
        {
            _languageDal.Delete(language);
            return new SuccessResult(Messages.LanguageDeleted);
        }

        public IResult Update(Language language)
        {
            _languageDal.Update(language);
            return new SuccessResult(Messages.LanguageUpdated);
        }
    }
}