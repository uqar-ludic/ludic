using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ludic_sandbox;

namespace Ludic_sandbox_test {
  [TestClass]
  public class Sandbox_test {
    [TestMethod]
    public void Execute_Simple_HelloWorld() {
      string code = @"
        using System;
        namespace Test {
          public class Program {
            public static int Main(){
              Console.WriteLine(""Hello, world!"");
              return 0;
            }
          }
        }
      ";
      Sandbox sb = new Sandbox(code);
      sb.Compile();
      int res = sb.Execute<int>("Test", "Program", "Main");
      Assert.AreEqual(sb.ConsoleText, "Hello, world!\r\n");
      Assert.AreEqual(res, 0);
    }
    [TestMethod]
    public void Execute_With_SimpleReturnValue() {
      string code = @"
        using System;
        namespace Test {
          public class Program {
            public static int Main(){
              Console.WriteLine(""Hello, world!"");
              return 42;
            }
          }
        }
      ";
      Sandbox sb = new Sandbox(code);
      sb.Compile();
      int res = sb.Execute<int>("Test", "Program", "Main");
      Assert.AreEqual(res, 42);
    }
    [TestMethod]
    public void Execute_MethodInClass() {
      string code = @"
        using System;
        namespace Test {
          public class Program {
            public static int Main(){
              Console.WriteLine(""Hello, world!"");
              return 0;
            }
            public int returnFortyTwo() {
              return 42;
            }
          }
        }
      ";
      Sandbox sb = new Sandbox(code);
      sb.Compile();
      int res = sb.Execute<int>("Test", "Program", "returnFortyTwo");
      Assert.AreEqual(res, 42);
    }
  }
}
