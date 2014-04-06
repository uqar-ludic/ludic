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
        
      
        // Voir si nécéssaire de faire un suivi des demandes d"exécution à partir d'ici ou pas ?

        #region Creation des permission (IPermission) à ajouter au PermissionSet
        // Creation de permissions, on y ajoute chaque ligne du fichier de permissions dans une liste avec laquelle on produit le dictionnaire des permissions.
        // NB: le fichier de permissions doit avoir au minimum le droit d'execution. chaque ligne reprèsente soit : write, read, execute ...

        public Dictionary<string, IPermission> CreatePermission(string permissionPath, string executablePath)
        {
            String[] permissions = File.ReadAllLines(permissionPath);
            List<string> tempPerm = new List<string>(permissions);
            return PreparePermission(tempPerm, executablePath);
        }


        private Dictionary<string, IPermission> PreparePermission(List<string> DemandPermissions, string PathExecutable)
        {
            // On regarde les permissions demandées et on associe l'objet qui crée la permission 
            Dictionary<string, IPermission> Permissions = new Dictionary<string, IPermission>();
            try
            {
                foreach (string item in DemandPermissions)
                {
                    switch (item.ToUpper())
                    {
                        case "WRITE":
                            //// créer un fichier
                            FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            f2.AddPathList(FileIOPermissionAccess.Write, UntrustedCodeFolderAbsolutePath);
                            Permissions.Add("CreateFile", f2);
                            break;
                        case "READ":
                            //// créer un fichier
                            FileIOPermission f3 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            f3.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            Permissions.Add("CreateFile", f3);
                            break;
                        case "CREATEFILE":
                            //// créer un fichier
                            FileIOPermission f4 = new FileIOPermission(FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            f4.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, UntrustedCodeFolderAbsolutePath);
                            Permissions.Add("CreateFile", f4);
                            break;
                    }
                }
                Permissions.Add("W-Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                Permissions.Add("W-UI", new UIPermission(PermissionState.Unrestricted));
                Permissions.Add("W-Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                Permissions.Add("W-Discover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                //// Permissions pour le fichier de résultats :
                Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                Permissions.Add("WRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                Permissions.Add("WUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
            }
            catch (SecurityException e)
            {
                throw (e);
            }
            return Permissions;
        }
        #endregion

        
        #region Execution du code
        // Initialise et ajout des permissions au PermissionSet du domain à crée.

        // Execute le code dans un nouveau domain.
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

class Sandboxer : MarshalByRefObject
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

        ObjectHandle handle = Activator.CreateInstanceFrom(newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName, typeof(Sandboxer).FullName);
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