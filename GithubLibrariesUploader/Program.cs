using Korn;
using Korn.Utils;

var kornPath = @"C:\Data\programming\vs projects\korn";
var overviewProjectPath = Path.Combine(kornPath, "Overview");
var librariesPath = Path.Combine(overviewProjectPath, "Binaries", "Libraries");

var cmd = new CommandLine();
cmd.WriteLine($"cd \"{overviewProjectPath}\"");
cmd.WriteLine($"git pull");

foreach (var file in Directory.GetFiles(librariesPath, "*.*", SearchOption.AllDirectories))
    if (Path.GetFileName(file) != "plug")
        File.Delete(file);

var libraries = Korn.Interface.ServiceModule.Libraries.DefaultLibraries;
foreach (var library in libraries)
{
    var projectName = library;
    var libraryProjectPath = Path.Combine(kornPath, projectName, projectName);
    if (!Directory.Exists(libraryProjectPath))
        throw new KornException(
            $"Unable find a project with name {projectName}."
        );

    var binariesPath = Path.Combine(libraryProjectPath, "bin", "x64", "Debug");
    var versionsDirectories = Directory.GetDirectories(binariesPath);

    var net8Directory = versionsDirectories.FirstOrDefault(directory => Path.GetFileName(directory) == "net8.0-windows");
    var net472Directory = versionsDirectories.FirstOrDefault(directory => Path.GetFileName(directory) == "net472");

    Move(net8Directory, Path.Combine(librariesPath, "net8"));
    Move(net472Directory, Path.Combine(librariesPath, "net472"));

    void Move(string? fromFolder, string toFolder)
    {
        if (fromFolder is null)
            return;

        foreach (var fileName in (string[])[projectName + ".dll", projectName + ".pdb"])
            File.Move(Path.Combine(fromFolder, fileName), Path.Combine(toFolder, fileName));
    }
}

cmd.WriteLine($"git add Binaries\\Libraries\\net8");
cmd.WriteLine($"git add Binaries\\Libraries\\net472");
cmd.WriteLine($"git commit -m \"auto: uploaded libraries\"");
cmd.WriteLine($"git push");
cmd.WriteLine($"git pull");