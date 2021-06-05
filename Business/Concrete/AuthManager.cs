using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Mailing;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        [ValidationAspect(typeof(UserForRegisterValidator), Priority = 1)]
        [MailAspect(Priority = 2)]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var userExist = UserExist(userForRegisterDto.Email);
            if (userExist.Equals(false))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    Email = userForRegisterDto.Email,
                    Firstname = userForRegisterDto.Firstname,
                    Lastname = userForRegisterDto.Lastname,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                _userService.Add(user);
                return new SuccessDataResult<User>(user, Messages.UserUpdated);    
            }
            return new ErrorDataResult<User>(Messages.UserAlreadyExist);
        }
        
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash,userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }
        
        [ValidationAspect(typeof(UserForUpdateValidator), Priority = 1)]
        public IDataResult<User> Update(UserForUpdateDto userForUpdateDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForUpdateDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Id = userForUpdateDto.Id,
                Email = userForUpdateDto.Email,
                Firstname = userForUpdateDto.Firstname,
                Lastname = userForUpdateDto.Lastname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Update(user);
            return new SuccessDataResult<User>(user, Messages.UserCreatedSuccessfuly);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims =_userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.TokenCreated);
        }
        private bool UserExist(string email)
        {
            var result = _userService.GetByMail(email);
            if (result.Data != null)
            {
                return true;
            }
            return false;
        }
    }
}
