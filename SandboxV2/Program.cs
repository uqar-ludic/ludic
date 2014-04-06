using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SandboxV2
{
    class Sandboxer : MarshalByRefObject
    {
        private PermissionSet setPermissions(string permissionPath, string executablePath)
        {
            string[] permissions = File.ReadAllLines(permissionPath);
            string UntrustedCodeFolderAbsolutePath = permissionPath.Substring(0, permissionPath.LastIndexOf('\\'));
            PermissionSet set = new PermissionSet(PermissionState.None);

            foreach (string perm in permissions)
            {
                switch (perm.ToUpper())
                {
                    case "WRITE":
                        FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                        f2.AddPathList(FileIOPermissionAccess.Write, UntrustedCodeFolderAbsolutePath);
                        set.AddPermission(f2);
                        break;
                    case "READ":
                        FileIOPermission f3 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                        f3.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                        set.AddPermission(f3);
                        break;
                    case "CREATEFILE":
                        FileIOPermission f4 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                        f4.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                        set.AddPermission(f4);
                        break;
                }
            }
            set.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            set.AddPermission(new UIPermission(PermissionState.Unrestricted));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, executablePath));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery, executablePath));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, @"D:\Result.txt"));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery, @"D:\Result.txt"));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Write, @"D:\Result.txt"));
            set.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
            return set;
        }

        const string untrustedAssembly = "AQGPI";
        const string untrustedClass = "AQGPI.Program";
        const string entryPoint = "Main";
        const string executablePath = @"C:\Users\Quentin\Documents\Visual Studio 2013\Projects\AQGPI\AQGPI\bin\Debug\";
        const string permissionPath = @"C:\Users\Quentin\Documents\Visual Studio 2013\Projects\AQGPI\AQGPI\bin\Debug\Permissions.txt";

        public void ExecuteSandBox(string permissionPath, string executablePath)
        {
            AppDomainSetup adSetup = new AppDomainSetup();
            adSetup.ApplicationBase = Path.GetFullPath(executablePath);

            PermissionSet permSet = setPermissions(permissionPath, executablePath);

            StrongName fullTrustAssembly = typeof(Sandboxer).Assembly.Evidence.GetHostEvidence<StrongName>();

            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

            ObjectHandle handle = Activator.CreateInstanceFrom(
                newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName,
                typeof(Sandboxer).FullName
                );
            Sandboxer newDomainInstance = (Sandboxer)handle.Unwrap();
            newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint);
        }

        public void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint)
        {
            MethodInfo target = Assembly.Load(assemblyName).GetType(typeName).GetMethod(Assembly.Load(assemblyName).EntryPoint.Name, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        try
        {
            FileStream ostrm = new FileStream(@"D:\Result.txt", FileMode.OpenOrCreate, FileAccess.Write); ;
            StreamWriter writer = new StreamWriter(ostrm);
            TextWriter oldOut = Console.Out;

            Console.SetOut(writer);
        
            target.Invoke(null, null);

            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {
            (new PermissionSet(PermissionState.Unrestricted)).Assert();
            Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
            CodeAccessPermission.RevertAssert();
        }
        Console.ReadKey();
        }
    }
}
