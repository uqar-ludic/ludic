using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;

namespace Ludic_sandbox {
  public class Sandbox : MarshalByRefObject {
    public string Code { get; set; }
    public string ConsoleText { get; set; }
    private CSharpCodeProvider _provider;
    private CompilerParameters _parameters;
    private Assembly _assembly;

    public Sandbox(string code) {
      Code = code;
      initSandbox();
    }

    private void initSandbox() {
      _provider = new CSharpCodeProvider();
      _parameters = new CompilerParameters();

      _parameters.ReferencedAssemblies.Add("System.Drawing.dll");
      _parameters.GenerateInMemory = true;
      _parameters.GenerateExecutable = true;
    }

    public void Compile() {
      CompilerResults results;
      results = _provider.CompileAssemblyFromSource(_parameters, Code);

      if (results.Errors.HasErrors) {
        StringBuilder sb = new StringBuilder();
        foreach (CompilerError error in results.Errors) {
          sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
        }
        throw new InvalidOperationException(sb.ToString());
      }
      _assembly = results.CompiledAssembly;
    }

    public T Execute<T>(string assemblyName, string className, string funcName, object[] parameters = null) {
      Type program = _assembly.GetType(assemblyName + "." + className);
      object instance = Activator.CreateInstance(program);
      MethodInfo main = program.GetMethod(funcName);
      TextWriter stdout = Console.Out;
      StringWriter sw = new StringWriter();
      Console.SetOut(sw);
      T res = (T)main.Invoke(instance, parameters);
      Console.SetOut(stdout);
      ConsoleText = sw.ToString();
      sw.Close();
      return res;
    }
  }
}
