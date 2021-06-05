using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IResult Add(User user);        
        IResult Update(User user);

        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(int id);

    }
}
