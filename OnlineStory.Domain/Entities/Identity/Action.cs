


using OnlineStory.Domain.Abstractions;

namespace OnlineStory.Domain.Entities.Identity;

public class Action: EntityBase<int>
{
    public string Name { get; set; } // Tên quyền (ví dụ: "Read", "Write", "Delete")
    private readonly List<Permission> _permissions;
    public  IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();
    private Action()
    {
        _permissions = new List<Permission>();
    }
    public Action(string name): this()
    {
        Name = name;
    }
}
