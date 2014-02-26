using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;
using SandBox;
using System.Threading;
using System.Runtime.Remoting;

namespace WebSiteSandBox
{
    /// <summary>
    /// Description résumée de SandBoxWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class SandBoxWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// DoWork : est une méthode qui crée une instance de SandBox est l'éxécute 
        /// </summary>
        /// <param name="cheminPermissions"> Le chemin du fihier text des permissions.</param>
        /// <param name="cheminExecutable"> Le chemin de l'exécutable </param>
        public static void DoWork(String cheminPermissions, String cheminExecutable)
        {

            SandBox.SandBox sb = new SandBox.SandBox();
            SandBox.SandBox.ExecuteCode(sb.CreatePermission(cheminPermissions, cheminExecutable), cheminExecutable);

        }

        [WebMethod]
        public string Execute(String cheminPermissions, String cheminExecutable)
        {
            try
            {
                try
                {
                    Thread t = new Thread(() => DoWork(cheminPermissions, cheminExecutable));
                    t.Start();
                    if (!t.Join(TimeSpan.FromSeconds(30)))
                    {
                        t.Abort();

                        throw new Exception("TimeOut dépassé !");

                    }

                }
                catch (Exception ex)
                {
                    return String.Format("Exception caught:\n{0}", ex.ToString());

                }
                // Voir comment gérer s'il y'a plusieurs resultats du même exercice.

                if (File.Exists(cheminExecutable + ".result.txt"))
                {
                    String result = File.ReadAllText(cheminExecutable + ".result.txt");
                    //File.Delete(cheminExecutable + ".result.txt");

                    return result;
                }
                else return "none";
            }
            catch (ApplicationException e)
            {
                e.ToString();
                return "failed";
            }
        }
    }
}
