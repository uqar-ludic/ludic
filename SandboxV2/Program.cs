﻿using System;
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
    public class Sandboxer : MarshalByRefObject
    {
        //Generate permissions for the execution in the new domain, according to the permission file givan as parameter
        private static PermissionSet setPermissions(string permissionPath, string executablePath)
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
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, executablePath + ".result.txt"));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery, executablePath + ".result.txt"));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Write, executablePath + ".result.txt"));
            set.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
            return set;
        }

        #region HardCodedVariables
        //This variables have to be replaced with :
        //          - untrustedAssembly => name of the solution you want to run
        //          - untrustedClass => name of the class you want to run with its assembly name
        //          - entryPoint => should always be the Main function of the program, can be changed
        const string untrustedAssembly = "AQGPI";
        const string untrustedClass = "AQGPI.Program";
        const string entryPoint = "Main";
        #endregion

        public static void ExecuteSandBox(string permissionPath, string executablePath)
        {
            //Setup the new domain with the path to the executable file as parameter 
            AppDomainSetup adSetup = new AppDomainSetup();
            adSetup.ApplicationBase = Path.GetFullPath(executablePath);

            //Set the permissions
            PermissionSet permSet = setPermissions(permissionPath, executablePath);

            //Get the StrongName of the assembly
            StrongName fullTrustAssembly = typeof(Sandboxer).Assembly.Evidence.GetHostEvidence<StrongName>();

            //Create the domain which will be used to run the assembly
            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

            //Allow to keep control of the newly created assembly in the curretn asssembly
            ObjectHandle handle = Activator.CreateInstanceFrom(
                newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName,
                typeof(Sandboxer).FullName
                );
            Sandboxer newDomainInstance = (Sandboxer)handle.Unwrap();
            newDomainInstance.ExecuteUntrustedCode(executablePath, untrustedClass, entryPoint);
        }

        public void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint)
        {
            //Get the assembly name and the method to run
            AssemblyName an = AssemblyName.GetAssemblyName(assemblyName);
            MethodInfo target = Assembly.Load(an).GetType(typeName).GetMethod(Assembly.Load(an).EntryPoint.Name, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            try
            {
                //Generate the output file
                FileStream ostrm = new FileStream(assemblyName + ".result.txt", FileMode.OpenOrCreate, FileAccess.Write); ;
                StreamWriter writer = new StreamWriter(ostrm);
                TextWriter oldOut = Console.Out;

                Console.SetOut(writer);
                
                //Invoke the method
                target.Invoke(null, null);

                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
            }
            catch (Exception ex)
            {
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                CodeAccessPermission.RevertAssert();
            }
        }
    }
}
