record GithubBinaryFolder(TargetVersion Version, string FolderDirectory)
{
    public void AddReference(string referencePath) => File.Copy(referencePath,Path.Combine(FolderDirectory, Path.GetFileName(referencePath)));
}