

using FluentValidation;
using static OnlineStory.Contract.Services.V1.Authentication.Query;

namespace OnlineStory.Contract.Services.V1.Authentication.Validators
{
    public class RefreshTokenValidator: AbstractValidator<RefreshTokenQuery>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.Device).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
