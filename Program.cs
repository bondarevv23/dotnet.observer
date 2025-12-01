using Microsoft.Build.Construction;

internal class Program
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
        if (!ValidateArgs(args)) return;
        
        var solutionPath = args[0];
        var solution = SolutionFile.Parse(solutionPath);
        var a = 1;
    }
}