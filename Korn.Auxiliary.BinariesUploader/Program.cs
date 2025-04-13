using static TargetVersion;
using Korn;

class Program
{
    static void Main() => new Program().InstanceMain();

    void InstanceMain()
    {
        const string
            KornDirectory = @"C:\Data\programming\vs projects\korn\",
            OverviewProjectDirectory = KornDirectory + @"Overview\",
            GithubProjectsRelativePath = "Binaries",
            GithubProjectsDirectory = OverviewProjectDirectory + GithubProjectsRelativePath;

        var nuget = new Nuget();
        var projects = new Projects(KornDirectory);
        var githubProjects = new GithubProjects(GithubProjectsDirectory);

        var p = nuget.GetPackage("Newtonsoft.Json", "net6.0");

        using var git = new GitManager(OverviewProjectDirectory);
        git.Pull();

        DeleteDirectories();
        CreateDirectories();
        CopyBinaries();

        git
        .Add(GithubProjectsRelativePath)
        .Commit("auto: uploaded binaries")
        .Push()
        .Pull();

        Thread.Sleep(5000);

        void DeleteDirectories() => Directory.Delete(GithubProjectsDirectory, true);

        void CreateDirectories() => githubProjects.GetAllDirectories().ForEach(directory => Directory.CreateDirectory(directory));

        void CopyBinaries() =>
            ((List<(Project from, GithubProject to)>)[
                (projects.Bootstrapper, githubProjects.Bootstrapper),
                (projects.ServiceHub, githubProjects.ServiceHub),
                (projects.InjectorService, githubProjects.InjectorService),
                (projects.LoggerService, githubProjects.LoggerService),
            ]).ForEach(entry => entry.from.CopyAllBinariesTo(entry.to));
    }
}