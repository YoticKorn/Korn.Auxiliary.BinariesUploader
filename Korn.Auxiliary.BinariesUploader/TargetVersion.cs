enum TargetVersion
{
    net472,
    net8
}

static class TargetVersionExtensions
{
    public static string ToDirectory(this TargetVersion self) => self == TargetVersion.net472 ? "net472" : "net8-windows";

    public static string ToGithubDirectory(this TargetVersion self) => self == TargetVersion.net472 ? "net472" : "net8";
}