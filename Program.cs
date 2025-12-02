using System.Runtime.CompilerServices;
using Microsoft.Build.Locator;
using Microsoft.Build.Construction;
// using ArgumentException = System.ArgumentException;
using Microsoft.CodeAnalysis.MSBuild;

public class Program
{
    private static bool ValidateArgs(string[] args)
    {
        List<string> validationErrors = [];

        ValidateArgsArePresented(args, validationErrors);
        ValidateCorrectSolutionPathIsPresented(args, validationErrors);

        if (validationErrors.Count == 0) return true;
        
        Console.WriteLine("The following validation error(s) occurred:");
        validationErrors.ConvertAll(x => "    " + x).ForEach(Console.WriteLine);
        Console.WriteLine("Usage: dotnet observe [solution-path] [dependency-name] [version]");
        return false;
    }

    private static void ValidateArgsArePresented(string[] args, List<string> validationErrors)
    {
        if (validationErrors.Count != 0) return;
        
        if (args.Length != 3) validationErrors.Add("3 args are expected");
        if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
            validationErrors.Add("SolutionPath should be provided");
        if (args.Length < 2 || string.IsNullOrEmpty(args[1]))
            validationErrors.Add("DependencyName should be provided");
        if (args.Length < 3 || string.IsNullOrEmpty(args[2]))
            validationErrors.Add("DependencyVersion should be provided");
    }
    
    private static void ValidateCorrectSolutionPathIsPresented(string[] args, List<string> validationErrors)
    {
        if (validationErrors.Count != 0) return;
        
        var solutionPath = args[0];
        
        if (!solutionPath.EndsWith(".sln"))
            validationErrors.Add("Solution file should end in .sln");
        if (!File.Exists(solutionPath))
            validationErrors.Add($"Solution file '{solutionPath}' not found");
    }
    
    public static void Main(string[] args)
    {
        var instance = MSBuildLocator.RegisterDefaults();
        
        // Console.WriteLine($"MSBuild Instance Found:");
        // Console.WriteLine($"  Version: {instance.Version}");
        // Console.WriteLine($"  Path: {instance.MSBuildPath}");

        ExecuteLogic(args);
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static async Task ExecuteLogic(string[] args)
    {
        if (!ValidateArgs(args)) return;

        var workspace = MSBuildWorkspace.Create();
        var solution = await workspace.OpenSolutionAsync(args[0]);
        var depGraph = solution.GetProjectDependencyGraph();
        // var solutionPath = args[0];
        // var solution = SolutionFile.Parse(solutionPath); // TODO handle exceptions
        // var solutionDir = Path.GetDirectoryName(solutionPath)!; // TODO ?
        // foreach (var project in solution.ProjectsInOrder)
        // {
        //     var projectPath = Path.Combine(solutionDir, project.RelativePath);
        //     var projectDir = Path.GetDirectoryName(projectPath)!; // TODO ?
        //     var assetsPath = Path.Combine(projectDir, "obj", "project.assets.json");
        //     if (!File.Exists(assetsPath))
        //     {
        //         throw new FileNotFoundException(
        //             $"project.assets.json not found at '{assetsPath}'." +
        //             $"Run 'dotnet restore' on the solution/project first.");
        //     }
        // }
        
        // Console.WriteLine($"Successfully parsed solution with {solution.ProjectsInOrder.Count} projects.");
    }
}
