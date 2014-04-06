using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;

namespace SandBox
{
    public class SandBox : MarshalByRefObject
    {
        public static string UntrustedCodeFolderAbsolutePath = Properties.Settings.Default.FilePath;
        public Dictionary<string, IPermission> Permissions;

        #region Creation des permission (IPermission) à ajouter au PermissionSet

        public Dictionary<string, IPermission> CreatePermission(string permissionPath, string executablePath)
        {
            String[] permissions = File.ReadAllLines(permissionPath);
            List<string> tempPerm = new List<string>(permissions);
            return PreparePermission(tempPerm, executablePath);
        }

        private Dictionary<string, IPermission> PreparePermission(List<string> DemandPermissions, string PathExecutable)
        {
            Dictionary<string, IPermission> Permissions = new Dictionary<string, IPermission>();
            try
            {
                foreach (string item in DemandPermissions)
                {
                    switch (item.ToUpper())
                    {
                        case "WRITE":
                            Permissions.Add("W-Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("W-UI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("W-Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("W-Discover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            //// Permissions pour le fichier de résultats :
                            Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("WRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("WUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));

                            break;
                        case "READ":

                            //// Permissions d'execution : 
                            Permissions.Add("R-Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("R-UI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("R-Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("R-Discover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            //// Permissions sur le fichier des résultats :
                            Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("WRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("WUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));

                            break;
                        case "EXECUTE":
                            //// Permissions d'execution : 
                            Permissions.Add("E-Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("E-UI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("E-Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("E-Discover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            //// Permissions sur le ficheir de résultats :
                            Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("RRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("EXWUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
                            break;
                        case "CREATEFILE":
                            //// créer un fichier
                            FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            Permissions.Add("CreateFile", f2);
                            break;
                        case "CREATEFILEWRITE":

                            ////Permissions d'execution : 
                            Permissions.Add("CW-Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("CW-UI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("CW-Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("CW-Discover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            //// Permissions pour le fichier de résultats :
                            Permissions.Add("CW-RRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("CW-RDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("CW-EcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("CW-UnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
                            //// Créer un fichier 
                            FileIOPermission file = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            file.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            Permissions.Add("CreateFile", file);
                            break;
                        default:

                            Permissions.Add("D-Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("D-UI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("D-Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("D-Discover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            break;
                    }
                }
            }
            catch (SecurityException e)
            {
                throw (e);
                // Console.WriteLine("Security Exception:\n\n{0}", e.Message); 
            }
            return Permissions;
        }
        #endregion

        #region Execution du code

        public static int ExecuteCode(Dictionary<string, IPermission> ListPersmissions, string Path)
        {

            PermissionSet permSet = new PermissionSet(PermissionState.None);

            foreach (KeyValuePair<string, IPermission> pair in ListPersmissions)
            {
                IPermission Permi = pair.Value;
                permSet.AddPermission(Permi);
            }

            return ExecuteDomain(permSet, Path, UntrustedCodeFolderAbsolutePath);

        }

        // Execution du sandbox dans un nouveau domain avec les permissions recues.
        private static int ExecuteDomain(PermissionSet permSet, string executableToTest, string UntrustedCodeFolderAbsolutePath)
        {

            AppDomainSetup adSetup = new AppDomainSetup();
            adSetup.ApplicationBase = Path.GetFullPath(UntrustedCodeFolderAbsolutePath);

            StrongName fullTrustAssembly = typeof(SandBox).Assembly.Evidence.GetHostEvidence<StrongName>();
            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);
            try
            {

                return newDomain.ExecuteAssembly(executableToTest);

            }
            catch (Exception ex)
            {
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                CodeAccessPermission.RevertAssert();
                throw ex;
            }
            finally
            {

                AppDomain.Unload(newDomain);

            }

        }
        #endregion

    }
}

public class Sandboxer : MarshalByRefObject
{
    const string pathToUntrusted = @"..\..\..\UntrustedCode\bin\Debug";
    const string untrustedAssembly = "UntrustedCode";
    const string untrustedClass = "UntrustedCode.UntrustedClass";
    const string entryPoint = "IsFibonacci";
    private static Object[] parameters = { 45 };
    static void Main()
    {
        AppDomainSetup adSetup = new AppDomainSetup();
        adSetup.ApplicationBase = Path.GetFullPath(pathToUntrusted);

        PermissionSet permSet = new PermissionSet(PermissionState.None);
        permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

        StrongName fullTrustAssembly = typeof(Sandboxer).Assembly.Evidence.GetHostEvidence<StrongName>();

        AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

        //Use CreateInstanceFrom to load an instance of the Sandboxer class into the
        //new AppDomain. 
        ObjectHandle handle = Activator.CreateInstanceFrom(
            newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName,
            typeof(Sandboxer).FullName
            );
        //Unwrap the new domain instance into a reference in this domain and use it to execute the 
        //untrusted code.
        Sandboxer newDomainInstance = (Sandboxer)handle.Unwrap();
        newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint, parameters);
    }
    public void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint, Object[] parameters)
    {
        MethodInfo target = Assembly.Load(assemblyName).GetType(typeName).GetMethod(entryPoint);
        try
        {
            bool retVal = (bool)target.Invoke(null, parameters);
        }
        catch (Exception ex)
        {
            (new PermissionSet(PermissionState.Unrestricted)).Assert();
            Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
            CodeAccessPermission.RevertAssert();
            Console.ReadLine();
        }
    }
}