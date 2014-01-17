using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;
using System.Runtime.Remoting;

// La classe SandBoxer doit dériver de MMarshalByRefObject pour pouvoir créer un AppDomain 

        class Sandboxer : MarshalByRefObject
        {
            const string pathToUntrusted = @"..\..\..\UntrustedCode\bin\Debug";
            const string untrustedAssembly = "UntrustedCode";
            const string untrustedClass = "UntrustedCode.UntrustedClass";
            const string entryPoint = "Exercices";
            private static Object[] parameters;
            public string init (int param)
            {
                string tmp;
                parameters = new Object[1];
                parameters[0] = param;

                // On met le AppDomainSetuo dans un dossier différent que celui du sandbox
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = Path.GetFullPath(pathToUntrusted);

                // On définit les autorisations, ici on donne seulement l'exécution, autorisation minimum
                PermissionSet permSet = new PermissionSet(PermissionState.None);
                permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
        
                //On ajoute le nom fort du sandbox, pour l'ajouter à liste de confiance totale
                 StrongName fullTrustAssembly = typeof(Sandboxer).Assembly.Evidence.GetHostEvidence<StrongName>();

                // On créé le AppDomain sur lequel on exuctera le code invité(utilisteur) avec les paramettres qu'on a créé en haut
                AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

                //On créé l'instance dans laquelle on va charger le code et le faire exécuter dans le nouveau AppDomain du sandbox
                ObjectHandle handle = Activator.CreateInstanceFrom(
                    newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName,
                    typeof(Sandboxer).FullName
                    );
                // On lance l'exécution dans le nouveau domaine
                Sandboxer newDomainInstance = (Sandboxer)handle.Unwrap();
                tmp = newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint, parameters);
                return tmp;
            }
            public string ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint, Object[] parameters)
            {
                // Avec MéthodeInfo, on charge la méthode de l'exercie pour l'exécuter dans une nouvelle assembly
                MethodInfo target = Assembly.Load(assemblyName).GetType(typeName).GetMethod(entryPoint);
                try
                {
                    // On récupère le résultat.
                    return (string)target.Invoke(null, parameters);
                }
                catch (Exception ex)
                {
                    // On cas de mauvais code, on le le catch et on peut voir de quel type est l'exception
                    string tmp;
                    (new PermissionSet(PermissionState.Unrestricted)).Assert();
                    tmp = "Accès non autorise! ouh ?" + "\n" + ex.ToString();
                    CodeAccessPermission.RevertAssert();
                    return tmp;
                }
       
            }
        }