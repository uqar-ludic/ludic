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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Diagnostics;
using System.Reflection;

namespace SandBox
{
    class SandBoxer
    {
        static void Main(string[] args)
        {
        }

    }

    public class SandBox : MarshalByRefObject
    {
        public static string UntrustedCodeFolderAbsolutePath = @"C:\HomeSandBox";
        public Dictionary<string, IPermission> Permissions;

        // Voir si nécéssaire de faire un suivi des demandes d"exécution à partir d'ici ou pas ?

        // Creation de permissions, on y ajoute chaque ligne du fichier de permissions dans une liste avec laquelle on produit le dictionnaire des permissions.
        // NB: le fichier de permissions doit avoir au minimum le droit d'execution. chaque ligne reprèsente soit : write, read, execute ...

        public Dictionary<string, IPermission> CreatePermission(string permissionPath, string executablePath)
        {
            String[] permissions = File.ReadAllLines(permissionPath);
            List<string> tempPerm = new List<string>(permissions);
            return PreparePermission(tempPerm, executablePath);
        }


        #region Creation des permission (IPermission) à ajouter au PermissionSet

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

                             //Permissions d'execution : 
                            
                            Permissions.Add("WExecute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("WUI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("WRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("WDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            // Permissions le ficheir de résultats :
                            Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("WRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("WUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));

                            break;
                        case "READ":

                            // Permissions d'execution : 
                            Permissions.Add("RExecute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("RUI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("RRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("RDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            // Permissions sur le ficheir de résultats :
                            Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("WRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("WUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));

                            //new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable);
                            break;
                        case "EXECUTE":
                            // Permissions d'execution : 
                            Permissions.Add("Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("RUI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("ERead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("EDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            // Permissions sur le ficheir de résultats :
                            Permissions.Add("ERRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("RRDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("EXWUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
                            break;
                        case "CREATEFILE":
                            // Permissions d'execution : 
                            Permissions.Add("Create", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("CUI", new UIPermission(PermissionState.Unrestricted));
                            Permissions.Add("CRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            Permissions.Add("CDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            // Permissions creation de fichier et fichier de resultat:
                            Permissions.Add("RRead", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable + ".Result.txt"));
                            Permissions.Add("CRERDiscover", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable + ".Result.txt"));
                            Permissions.Add("WEcrireDansUnFchier", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable + ".Result.txt"));
                            Permissions.Add("CREWUnmanagedCode", new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
                            //Permissions.Add("Createfile", new FileIOPermission(FileIOPermissionAccess.Read, "C:\\HomeSandBox"));

                            FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, @"C:\HomeSandBox");
                            f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, @"C:\HomeSandBox");
                            Permissions.Add("CreateFile", f2);
                            break;

                        default:
                            //Permissions.Add("Execute", new SecurityPermission(SecurityPermissionFlag.Execution)); 
                            break;
                    }
                }

                //return Permissions;
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
        // Initialise et ajout des permissions au PermissionSet du domain à crée.

        // Execute le code dans un nouveau domain. Le résultat de l'exécution sera mis dans un ficheir texte au nom C/.../NomDeLexecutable.result.txt 
        //dans le même dossier ou se trouve l'executable. Pour faire cela, il faut rajouter ces lignes en dessous dans le main du programme.

        //  StreamWriter result = new StreamWriter(System.Reflection.Assembly.GetEntryAssembly().Location+".result.txt");
        //  Console.SetOut(result);
        //   result.Flush();
        //   result.Close();

        public static int ExecuteCode(Dictionary<string, IPermission> ListPersmissions, string Path)
        {
           
            PermissionSet permSet = new PermissionSet(PermissionState.None);

            foreach (KeyValuePair<string, IPermission> pair in ListPersmissions)
            {
                IPermission Permi = pair.Value;
                permSet.AddPermission(Permi);
            }
           
           return ExecuteDomain(permSet, Path, UntrustedCodeFolderAbsolutePath);
            // le retour en string est pour le suivi ! 
           
        }

        // Execution du sandbox dans un nouveau domain avec les permissions recue.

        private static int ExecuteDomain(PermissionSet permSet, string executableToTest, string UntrustedCodeFolderAbsolutePath)
        {

            //TextWriter originalConsoleOutput = Console.Out;
            //StringWriter writer = new StringWriter();
            //Console.SetOut(writer);

            AppDomainSetup adSetup = new AppDomainSetup();

            adSetup.ApplicationBase = Path.GetFullPath(UntrustedCodeFolderAbsolutePath);


            StrongName fullTrustAssembly = typeof(SandBox).Assembly.Evidence.GetHostEvidence<StrongName>();
            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);
            try
            {
                
                //byte[] bytes = File.ReadAllBytes(executableToTest);
                //Assembly assembly = Assembly.Load(bytes);
                //MethodInfo main = assembly.EntryPoint;
                //main.Invoke(null, new object[] { null });

                return  newDomain.ExecuteAssembly(executableToTest);
                
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
                //Console.SetOut(originalConsoleOutput);
                //string result = writer.ToString();
            }


        }
        #endregion

    }
}
