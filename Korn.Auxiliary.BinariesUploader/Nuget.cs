record Nuget(string RootDirectory) : IComparer<string?>
{
    public Nuget() : this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget")) { }

    public NugetPackage GetPackage(string name, string version) => new(name, GetPackageDirectory(Path.Combine(RootDirectory, "packages", name), version));

    string GetPackageDirectory(string path, string targetVersion) => Path.Combine(GetPackageVersionDirectory(path), "lib", targetVersion);
    
    string GetPackageVersionDirectory(string path) => Directory.GetDirectories(path).OrderBy(Path.GetFileName, this).First();

    public int Compare(string? x, string? y) => GetScore(y) - GetScore(x);

    int GetScore(string? input) =>
        input is null
        ? 0
        : input.Split('.')
          .Select((part, i) => (localScore: int.Parse(part), i))
          .Sum(entry => entry.localScore * (int)Math.Pow(100, entry.i));
}