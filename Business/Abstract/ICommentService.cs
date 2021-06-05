using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetByMovieId(int id);
        IDataResult<List<Comment>> GetBySubId(int id);
        IDataResult<List<Comment>> GetAllPublished();
        IResult Add(Comment comment);
        IResult Delete(Comment comment);
        IResult Update(Comment comment);
    }
}
