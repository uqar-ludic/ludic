﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using RemotingInterface;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Reflection;

namespace SandBox
{
    class SandBoxer
    {
        static void Main(string []args)
        {
            SoapServerFormatterSinkProvider prov = new SoapServerFormatterSinkProvider();
            prov.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            IDictionary props = new Hashtable();
            props["port"] = 8081;
            HttpChannel chan = new HttpChannel(props, null, prov);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SandBox), "SandBox.soap",
            WellKnownObjectMode.SingleCall);

            // Loads the configuration file.
            //RemotingConfiguration.Configure("SandBox.exe.config");
            // à voir comment gérer le GC , ainsi que les threads 
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

    }

    public  class SandBox : MarshalByRefObject

    {
        public static string UntrustedCodeFolderAbsolutePath = @"C:\HomeSandBox";
        public Dictionary<string, IPermission> Permissions;
        //public static string executableToTest = "";


        public void ConnexionSandbox(InformationsUtil infoUtil)
        { 
            // Routine pour mettre les info dans la BD, 

            // pour le moment on se contente d'afficher seulement les données.

            Console.WriteLine("Message\n NomUtilisateur : {0}\n Date : {1}\n Exercice : {2}",
                                infoUtil.Nomutil, infoUtil.dateSoumi,
                                infoUtil.Exercise);
        }

        // Creer les permissions

        public Dictionary<string, IPermission> CreatePermission(string path)
        {
            List<string> tempPerm = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    tempPerm.Add(line);
                }
            }
            return PreparePermission(tempPerm, UntrustedCodeFolderAbsolutePath);
         
        }


        #region Creation des permission (Ipermission) à ajouter au PermissionSet

        private Dictionary<string, IPermission> PreparePermission(List<string> DemandPermissions, string PathExecutable)
        {
            // On regarde les permission demandées et on associe l'objet qui crée la permission 
            Dictionary<string, IPermission> Permissions = new Dictionary<string, IPermission>();
            try
            {

                foreach (string item in DemandPermissions)
                {

                    switch (item.ToUpper())
                    {
                        case "WRITE":
                            Permissions.Add("Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable));
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            break;
                        case "READ":
                            Permissions.Add("Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            Permissions.Add("Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable); break;
                        case "READWRITE":
                            Permissions.Add("Execute", new SecurityPermission(SecurityPermissionFlag.Execution));
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            Permissions.Add("ReadWrite", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable); break;
                        default:
                            Permissions.Add("Execute", new SecurityPermission(SecurityPermissionFlag.Execution)); break;
                    }
                }

                return Permissions;
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return Permissions;

        }

        #endregion

        // Créer les permissions de l'exercie.

        public string ExcuteCode(Dictionary<string, IPermission> ListPersmissions, string Path)
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

        private static string ExecuteDomain(PermissionSet permSet, string executableToTest, string UntrustedCodeFolderAbsolutePath)
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
