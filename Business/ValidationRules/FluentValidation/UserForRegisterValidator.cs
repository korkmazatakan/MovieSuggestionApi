using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserForRegisterValidator:AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator()
        {
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().Length(8,40);
            RuleFor(u => u.Firstname).NotEmpty().Length(1, 50);
            RuleFor(u => u.Lastname).NotEmpty().Length(1, 50);
        }
    }
}