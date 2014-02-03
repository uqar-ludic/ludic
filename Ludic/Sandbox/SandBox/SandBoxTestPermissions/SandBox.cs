using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;




namespace SandBoxTestPermissions
{
    public abstract class SandBox : MarshalByRefObject
    {
        //public static string UntrustedCodeFolderAbsolutePath = @"C:\HomeSandBox";
        //public static string executableToTest = "";

        static void Main()
        {

        }
        // Créer les permissions de l'exercie.

        public static string ExcuteCode(Dictionary<string, IPermission> ListPersmissions, string Path)
        {
            string resultat = "";

            PermissionSet permSet = new PermissionSet(PermissionState.None);

            foreach (KeyValuePair<string, IPermission> pair in ListPersmissions)
            {
             
                IPermission Permi = pair.Value;
                permSet.AddPermission(Permi);
            }

            resultat = ExecuteDomain(permSet, Path, Path);
            return resultat.ToString();
        }

        // Execution du sandbox dans un nouveau domain avec les permissions recue.

        public static string ExecuteDomain(PermissionSet permSet, string executableToTest, string UntrustedCodeFolderAbsolutePath)
        {

            AppDomainSetup adSetup = new AppDomainSetup();

            adSetup.ApplicationBase = Path.GetFullPath(UntrustedCodeFolderAbsolutePath);


            StrongName fullTrustAssembly = typeof(SandBox).Assembly.Evidence.GetHostEvidence<StrongName>();
            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);
            try
            {

                int result = newDomain.ExecuteAssembly(executableToTest);
                return result.ToString();
            }
            catch (Exception ex)
            {
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                CodeAccessPermission.RevertAssert();
                Console.ReadLine();
                return "";
            }
            finally
            {

                AppDomain.Unload(newDomain);
            }


        }

    }
}
