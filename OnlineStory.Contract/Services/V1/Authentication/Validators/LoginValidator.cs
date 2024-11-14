using FluentValidation;
using static OnlineStory.Contract.Services.V1.Authentication.Query;

namespace OnlineStory.Contract.Services.V1.Authentication.Validators;

public class LoginValidator: AbstractValidator<LoginQuery>
{
    public LoginValidator()
    {
        RuleFor(x => x.Device).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.UserNameOrEmail).NotEmpty();
    }
}
