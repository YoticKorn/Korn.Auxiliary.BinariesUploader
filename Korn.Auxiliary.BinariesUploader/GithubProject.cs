record GithubProject(string RootDirectory, params TargetVersion[] TargetVersions)
{
    public string GetBinaryDirectory(TargetVersion targetVersion) => Path.Combine(RootDirectory, targetVersion.ToGithubDirectory());

    public string[] GetBinaryDirectories() => TargetVersions.Select(GetBinaryDirectory).ToArray();

    public GithubBinaryFolder GetBinaryFolder(TargetVersion targetVersion) => new(targetVersion, GetBinaryDirectory(targetVersion));

    public GithubBinaryFolder[] GetBinaryFolders() => TargetVersions.Select(GetBinaryFolder).ToArray();

    public void AddReference(string referencePath) => GetBinaryFolders().ToList().ForEach(binaryFolder => binaryFolder.AddReference(referencePath));
}