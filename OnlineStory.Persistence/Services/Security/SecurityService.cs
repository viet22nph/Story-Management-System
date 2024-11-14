
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineStory.Application.Abstractions.Security;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Persistence.ApplicationDbContext;
using StackExchange.Redis;
using System.Text.Json;

namespace OnlineStory.Persistence.Services.Security;

public class SecurityService : ISecurityService
{

    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICacheManager _cacheManager;
    public SecurityService(AppDbContext context, UserManager<AppUser> userManager, ICacheManager cacheManager)
    {
        _context = context;
        _userManager = userManager;
        _cacheManager = cacheManager;
    }
    public async Task<bool> UserHasPermissionAsync(string userId, string resource, string action)
    {

        var cacheKey = $"permissions:{userId}-{resource}-{action}";
        var cachedPermission = await _cacheManager.GetAsync(cacheKey);

        // Check if permission is cached in Redis
        if (!string.IsNullOrEmpty(cachedPermission))
        {
            return bool.Parse(cachedPermission);
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            return false;
        }
        var cacheKeyRole = $"roles:{userId}";
        var roles = await GetRolesFromCacheOrDbAsync(userId, cacheKeyRole); 
        var hasPermission = (from role in _context.Roles
                             join permission in _context.Permissions on role.Id equals permission.RoleId
                             join actionPermission in _context.Actions on permission.ActionId equals actionPermission.Id
                             join res in _context.Resources on permission.ResourceId equals res.Id
                             where roles.Contains(role.Name)
                             && res.Name == resource.ToString()
                             && actionPermission.Name == action.ToString()
                             select permission).Any();
        await _cacheManager.SetAsync(cacheKey, hasPermission, 15);
        return hasPermission;


    }
    private async Task<List<string>> GetRolesFromCacheOrDbAsync(string userId, string cacheKeyRoles)
    {
        var cachedRoles = await _cacheManager.GetAsync<List<string>>(cacheKeyRoles);
        if (cachedRoles is not null)
        {
            return cachedRoles;
        }
        // Fetch roles from database if not in cache
        var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
        var roleList = roles.ToList();

        // Cache roles in Redis
        await _cacheManager.SetAsync(cacheKeyRoles, roleList, 30);

        return roleList;
    }


}
