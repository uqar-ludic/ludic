using System;
using System.Collections.Generic;
using System.Linq;
using SandBox;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.Text;

namespace WebSandBox
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "SandBoxService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez SandBoxService.svc ou SandBoxService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class SandBoxService : ISandBoxService
    {
        string ExecuteCode(string Permissions, string path)
        {
            SandBox.SandBox sb = new SandBox.SandBox();
            
            return sb.ExcuteCode(sb.CreatePermission(Permissions), path);
        }
    }
}
