record BinaryFolder(string ProjectName, TargetVersion Version, string FolderDirectory)
{
    string[] GetFiles(string name, string[] extensions) =>
        Directory.GetFiles(FolderDirectory)
        .Where(file => Path.GetFileName(file) == name)
        .Where(file => extensions.Contains(Path.GetExtension(file)))
        .ToArray();

    string[] GetFiles(string[] extensions) => 
        Directory.GetFiles(FolderDirectory)
        .Where(file => extensions.Contains(Path.GetExtension(file)))
        .ToArray();

    public string[] GetExecutables() => GetFiles(ProjectName, [".pdb", ".exe", ".dll"]);

    public string[] GetAllExecutables() => GetFiles([".pdb", ".exe", ".dll"]);

    public string[] GetAllExecutablesNames() => GetAllExecutables().Select(Path.GetFileName).ToArray()!;

    public string[] GetExecutablesNames() => GetExecutables().Select(Path.GetFileName).ToArray()!;

    public void CopyAllExecutablesTo(string to) => CopyTo(FolderDirectory, to, GetAllExecutablesNames());

    public void CopyAllExecutablesTo(GithubBinaryFolder to) => CopyTo(FolderDirectory, to.FolderDirectory, GetAllExecutablesNames());

    public void CopyExecutablesTo(string to) => CopyTo(FolderDirectory, to, GetExecutablesNames());

    public void CopyExecutablesTo(GithubBinaryFolder to) => CopyTo(FolderDirectory, to.FolderDirectory, GetExecutablesNames());

    public string GetFile(string fileName) => Path.Combine(FolderDirectory, fileName);

    void CopyTo(string from, string to, string[] fileNames) => fileNames.ToList().ForEach(name => File.Copy(Path.Combine(from, name), Path.Combine(to, name)));
}