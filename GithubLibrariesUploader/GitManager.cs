using Korn.Utils;

class GitManager : IDisposable
{
    public GitManager(string projectPath)
    {
        cmd = new CommandLine();
        cmd.WriteLine($"cd \"{projectPath}\"");
    }

    CommandLine cmd;

    GitManager Execute(string line)
    {
        cmd.WriteLine(line);
        return this;
    }

    public GitManager Pull() => Execute("git pull");
    public GitManager Push() => Execute("git push");
    public GitManager Add(string path) => Execute($"git add {path}");
    public GitManager Commit(string message) => Execute($"git commit -m \"{message}\"");

    #region IDisposable
    bool disposed;
    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;

        cmd.Dispose();
    }

    ~GitManager() => Dispose();
    #endregion
}