using static TargetVersion;

record GithubProjects(string RootDirectory)
{
    GithubProject GetFolder(string name, params TargetVersion[] versions) => new(Path.Combine(RootDirectory, name), versions);

    GithubProject GetServiceFolder(string name, params TargetVersion[] versions) => new(Path.Combine(RootDirectory, "Services", name), versions);

    public GithubProject Bootstrapper => GetFolder("Bootstrapper", net8, net472);

    public GithubProject ServiceHub => GetFolder("ServiceHub", net472);

    public GithubProject InjectorService => GetServiceFolder("Injector", net8);

    public GithubProject LoggerService => GetServiceFolder("Logger", net8);

    public GithubProject[] GetAllProjects() => [Bootstrapper, ServiceHub, InjectorService, LoggerService];

    public List<string> GetAllDirectories() => GetAllProjects().SelectMany(project => project.GetBinaryDirectories()).ToList();
}