using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCompilationLudic
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyExecutionPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            Compilation.CompilationLudic.Compile(@"C:\Users\Quentin\Documents\Visual Studio 2013\Projects\AQGPI\AQGPI.sln",
                @"C:\Users\Quentin\Desktop\build.log");
                //assemblyExecutionPath.Remove(assemblyExecutionPath.LastIndexOf("\\") + 1) + "build.log");
        }
    }
}
