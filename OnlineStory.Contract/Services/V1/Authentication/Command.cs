using OnlineStory.Contract.Abstractions.Message;

namespace OnlineStory.Contract.Services.V1.Authentication
{
    public static class Command
    {
        public record RegisterCommand(string Email, string UserName, string Password, string ConfirmPassword) : ICommand<Success>;
    }
}
