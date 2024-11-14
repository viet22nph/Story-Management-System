
namespace OnlineStory.Domain.Entities.Identity;

public class Permission
{
    public Guid RoleId { get; set; }
    public virtual AppRole Role { get; set; } // Mối quan hệ với IdentityRole

    public int ActionId { get; set; }
    public virtual Action Action { get; set; } // Mối quan hệ với quyền

    public int ResourceId { get; set; }
    public virtual Resource Resource { get; set; }
}
