using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IInMovieImageService
    {
        IDataResult<InMovieImage> GetById(string id);
        IDataResult<IList<InMovieImage>> GetByMovieId(int movieId);
        IResult Add(InMovieImage image);
        IResult Delete(InMovieImage image);
        IResult Update(InMovieImage image);
    }
}
