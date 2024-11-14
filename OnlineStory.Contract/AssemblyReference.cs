using System.Reflection;

namespace OnlineStory.Contract;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
