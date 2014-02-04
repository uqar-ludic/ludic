using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.Text;

namespace WebSandBox
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "ISandBoxService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface ISandBoxService
    {
        [OperationContract]
        string ExecuteCode(string Permissions, string path);
    }
}
