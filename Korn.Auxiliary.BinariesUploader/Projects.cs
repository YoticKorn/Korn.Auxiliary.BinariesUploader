record Projects(string RootDirectory)
{
    public string GetSoultionDirectory(string name) => Path.Combine(RootDirectory, name);

    public string GetProjectDirectory(string name) => Path.Combine(GetSoultionDirectory(name), name);

    public Project GetProject(string name) => new(name, GetProjectDirectory(name));

    public Project Bootstrapper => GetProject("Korn.Bootstrapper");

    public Project ServiceHub => GetProject("Korn.WinServices.ServiceHub");

    public Project InjectorService => GetProject("Korn.Services.Injector");

    public Project LoggerService => GetProject("Korn.Services.Logger");
}