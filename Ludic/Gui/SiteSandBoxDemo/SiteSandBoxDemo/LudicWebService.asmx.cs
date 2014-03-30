using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace SiteSandBoxDemo
{
    /// <summary>
    /// Summary description for LudicWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LudicWebService : System.Web.Services.WebService
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
        public string CompilSln(string slnPath, string logPath)
        {
            Compil.Compil.ExecuteCompil(slnPath, logPath);
            return File.ReadAllText(logPath);
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
