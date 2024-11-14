
using FluentValidation;
using static OnlineStory.Contract.Services.V1.Authentication.Command;
namespace OnlineStory.Contract.Services.V1.Authentication.Validators;

public class RegisterValidator: AbstractValidator<RegisterCommand>
{
    RegisterValidator()
    {
        RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Please enter the confirmation username")
              .NotNull()
              .EmailAddress();
        RuleFor(m => m.UserName)
            .NotEmpty()
            .WithMessage("Please enter the confirmation username")
            .Length(3, 25)
            .Must(userName => !userName.All(c => char.IsWhiteSpace(c)))
            .WithMessage("UserName must not contain whitespace meaning");
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("Please enter the confirmation password")
            .MinimumLength(6)
            .MaximumLength(50);
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Please enter the confirmation password");
        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.Password != x.ConfirmPassword)
            {
                context.AddFailure(nameof(x.Password), "Passwords should match");
            }
        });
    }
}
