using System;

namespace FreeSource
{
    internal static class Program
    {
        private static void Main(string[] arguments)
        {
            arguments = new string[1] { "Test.txt" };

            VisualStudioSolution solution = new VisualStudioSolution(arguments[0]);
        }
    }
}
