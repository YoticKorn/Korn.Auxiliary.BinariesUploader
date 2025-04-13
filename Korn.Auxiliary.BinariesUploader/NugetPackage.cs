record NugetPackage(string PackageName, string RootDirectory)
{
    string[] GetFiles(string name, string[] extensions) =>
        Directory.GetFiles(RootDirectory)
        .Where(file => Path.GetFileName(file) == name)
        .Where(file => extensions.Contains(Path.GetExtension(file)))
        .ToArray();

    public string[] GetExecutables() => GetFiles(PackageName, [".pdb", ".exe", ".dll"]);

    public string[] GetExecutablesNames() => GetExecutables().Select(Path.GetFileName).ToArray()!;

    public void CopyExecutablesTo(string to) => CopyTo(RootDirectory, to, GetExecutablesNames());

    public void CopyExecutablesTo(GithubBinaryFolder to) => CopyTo(RootDirectory, to.FolderDirectory, GetExecutablesNames());

    public void CopyExecutablesTo(GithubProject to) => to.GetBinaryFolders().ToList().ForEach(CopyExecutablesTo);

    void CopyTo(string from, string to, string[] fileNames) => fileNames.ToList().ForEach(name => File.Copy(Path.Combine(from, name), Path.Combine(to, name)));
}