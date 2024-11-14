
namespace OnlineStory.Application.Abstractions.Security;

public interface ISecurityService
{
    Task<bool> UserHasPermissionAsync(string userId, string resource, string action);
}
