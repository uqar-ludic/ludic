using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;
using System.Threading;
using System.Runtime.Remoting;
using SandboxV2;

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
        //Compile sln and return the log as text
        [WebMethod]
        public string CompilSln(string slnPath, string logPath)
        {
            Compil.Compil.ExecuteCompil(slnPath, logPath);
            return File.ReadAllText(logPath);
        }

        //Execute the assembly in a new thread and return the log as text
        [WebMethod]
        public string Execute(String cheminPermissions, String cheminExecutable)
        {
            Thread th = new Thread(() => Sandboxer.ExecuteSandBox(cheminPermissions, cheminExecutable));
            th.Start();
            if (!th.Join(TimeSpan.FromSeconds(30)))
                th.Abort();
            return File.ReadAllText(cheminExecutable + ".result.txt");
        }
    }
}
