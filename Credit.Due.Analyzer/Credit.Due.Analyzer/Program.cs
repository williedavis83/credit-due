using System.Reflection;

namespace Credit.Due.Analyzer;

public class Program
{
    public static void Main(string?[] args)
    {
        if (args == null || args.Length == 0)
            args = new[] { Assembly.GetExecutingAssembly().FullName };
        foreach (var arg in args)
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load(arg!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                continue;
            }
            Console.WriteLine($"Asssembly: {assembly.FullName}");
            foreach (var credit in AssemblyAnalyzer.GetTotalCredits(assembly))
                Console.WriteLine($"{credit}");
        }
    }
}