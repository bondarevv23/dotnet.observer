internal class Program
{
    private static bool ValidateArgs(string[] args)
    {
        var argsAreValid = true;
        if (args.Length != 3)
        {
            argsAreValid  = false;
            Console.Error.WriteLine("3 args are expected");
        }

        if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
        {
            argsAreValid  = false;
            Console.Error.WriteLine("SolutionPath should be provided");
        }
        
        if (args.Length < 2 || string.IsNullOrEmpty(args[1]))
        {
            argsAreValid  = false;
            Console.Error.WriteLine("DependencyName should be provided");
        }

        if (args.Length < 3 || string.IsNullOrEmpty(args[2]))
        {
            argsAreValid  = false;
            Console.Error.WriteLine("DependencyVersion should be provided");
        }

        if (!argsAreValid)
        {
            Console.WriteLine("Usage: dotnet observe [solution-path] [dependency-name] [version]");
        }
        
        return argsAreValid;
    }
    
    public static void Main(string[] args)
    {
        if (!ValidateArgs(args)) return;
        
        
    }
}