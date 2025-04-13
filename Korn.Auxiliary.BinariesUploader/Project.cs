record Project(string ProjectName, string ProjectDirectory)
{
    public string BinaryVersionsDirectory => Path.Combine(ProjectDirectory, "bin", "x64", "Debug");

    public string GetBinaryDirectory(TargetVersion targetVersion) => Path.Combine(BinaryVersionsDirectory, targetVersion.ToDirectory());

    public BinaryFolder[] GetBinaryFolders() => 
        ((TargetVersion[])[TargetVersion.net8, TargetVersion.net472])
        .Where(version => Directory.Exists(GetBinaryDirectory(version)))
        .Select(GetBinaryFolder)
        .ToArray();

    public TargetVersion[] GetVersions() => GetBinaryFolders().Select(folder => folder.Version).ToArray();

    public BinaryFolder GetBinaryFolder(TargetVersion targetVersion) => new(ProjectName, targetVersion, GetBinaryDirectory(targetVersion));

    public BinaryFolder Net8BinaryFolder => GetBinaryFolder(TargetVersion.net8);
    public BinaryFolder Net472BinaryFolder => GetBinaryFolder(TargetVersion.net472);

    public void CopyAllBinariesTo(GithubProject to) =>
        GetBinaryFolders()
        .ToList()
        .ForEach(binaryFolder => binaryFolder.CopyAllExecutablesTo(to.GetBinaryFolder(binaryFolder.Version)));

    public void CopyBinariesTo(GithubProject to) =>
       GetBinaryFolders()
       .ToList()
       .ForEach(binaryFolder => binaryFolder.CopyExecutablesTo(to.GetBinaryFolder(binaryFolder.Version)));
}