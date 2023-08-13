using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Credit.Due.Analyzer
{
    public sealed class AssemblyAnalyzer
    {
        public static IEnumerable<CreditDueAttribute> GetCredits(Assembly assembly)
        {
            foreach(var attribute in assembly.GetCustomAttributes(typeof(CreditDueAttribute), true))
                yield return (CreditDueAttribute)attribute;
        }

        public static IEnumerable<CreditDueAttribute> GetTotalCredits(Assembly assembly)
        {
            foreach (var attribute in GetCredits(assembly))
                yield return attribute;

            foreach (var dependencyName in assembly.GetReferencedAssemblies())
            {
                Assembly dependentAssembly;
                try
                {
                    dependentAssembly = Assembly.Load(dependencyName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
                foreach (var attribute in GetCredits(dependentAssembly))
                    yield return attribute;
            }
        }
    }
}
