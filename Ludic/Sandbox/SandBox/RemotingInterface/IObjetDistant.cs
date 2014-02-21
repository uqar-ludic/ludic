using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Threading.Tasks;

namespace RemotingInterface
{
    public interface IObjetDistant 

    {

         void ConnexionSandbox(InformationsUtil infoUtil);
         // Ici, on y trouve les interface du sandbox 

        // methode d'excution du code, la classe sandbox doit implimenter cette methode.

         string ExcuteCode(Dictionary<string, IPermission> ListPersmissions, string Path);

        // Voir aussi si on va intégrer la préparation des permissions ?  voir avec Steven ? 

    }

       // Informations liée à l'étudiant et l'exercice 

         [Serializable]
         public class InformationsUtil
         {
             public string Nomutil;      // nom d'utilisateur 
             public DateTime dateSoumi; // date de soumission de l'exercie
             public string Exercise;    // numero d'exercice

             public InformationsUtil(string NomUtilisateur, DateTime DateSoumission, string Exercice)
             {
                 this.Nomutil = NomUtilisateur; this.dateSoumi = DateSoumission; this.Exercise = Exercice; 
             }

             public InformationsUtil() { }

         }
    
}
