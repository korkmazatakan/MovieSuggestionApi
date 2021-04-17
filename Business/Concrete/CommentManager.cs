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

namespace Business.Concrete
{
    public class CommentManager:ICommentService
    {
        private ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IDataResult<List<Comment>> GetByMovieId(int id)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetList(movie => movie.MovieId == id).ToList());
        }

        public IDataResult<List<Comment>> GetBySubId(int id)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetList(movie => movie.SubCommentOf == id).ToList());
        }

        public IDataResult<List<Comment>> GetAllPublished()
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetList(movie => movie.IsPublished).ToList());
        }

        public IResult Add(Comment comment)
        {

            _commentDal.Add(comment);
            return new SuccessResult(Messages.CommentAdded);
        }

        public IResult Delete(Comment comment)
        {
            _commentDal.Delete(comment);
            return new SuccessResult(Messages.CommentDeleted);
        }

        public IResult Update(Comment comment)
        {
            _commentDal.Update(comment);
            return new SuccessResult(Messages.CommentUpdated);
        }
    }
}
