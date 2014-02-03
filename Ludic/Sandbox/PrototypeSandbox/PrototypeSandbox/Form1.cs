using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Runtime.Remoting;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using RemotingInterface;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.Remoting.Lifetime;

namespace PrototypeSandbox
{
    public partial class Form1 : Form
    {

        // Variables 
        public string PathexecutableSource = "";
        public string PathExecutable = "";
        public Dictionary<string, IPermission> Permissions;
        //private RemotingInterface.IObjetDistant ExcuteCode;

        InformationsUtil InfoUtilisateur = new InformationsUtil();
        IObjetDistant ObjDist = (IObjetDistant)Activator.GetObject(typeof(IObjetDistant), "http://localhost:8081/Sandbox.soap");
  
        public Form1()
        {
            InitializeComponent();

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel, false);
            IObjetDistant ObjLoc = (IObjetDistant)Activator.GetObject(typeof(IObjetDistant), "http://localhost:8081/Sandbox.soap");
       
            ChannelServices.UnregisterChannel(channel);

            // Charger les infos du fichier de configuration 

            //RemotingConfiguration.Configure("PrototypeSandbox.exe.config");
            //ClientActivatedType CAObject = new ClientActivatedType();
            //ILease serverLease = (ILease)RemotingServices.GetLifetimeService(CAObject);
            //MyClientSponsor sponsor = new MyClientSponsor();
            //serverLease.Register(sponsor);


            // informations utilisateur, à voir ce qu'il y'a lieu de rajouter comme infos

            InfoUtilisateur.Nomutil = "Utilisateur";
            InfoUtilisateur.dateSoumi = DateTime.Now;
            InfoUtilisateur.Exercise = "Exercice numero 1";

        }


        //#region Création du sponsor avec le bail 
        //public class MyClientSponsor : MarshalByRefObject, ISponsor
        //{

        //    private DateTime lastRenewal;

        //    public MyClientSponsor()
        //    {
        //        lastRenewal = DateTime.Now;
        //    }

        //    public TimeSpan Renewal(ILease lease)
        //    {

        //        //Console.WriteLine("I've been asked to renew the lease.");
        //        //Console.WriteLine("Time since last renewal:" + (DateTime.Now - lastRenewal).ToString());

        //        lastRenewal = DateTime.Now;
        //        return TimeSpan.FromSeconds(20);
        //    }
        //}
        //#endregion
        
        #region Chercher l'exécutable de la solution et le transfrer dans un dossier temp

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "exe files (*.exe)|*.exe|All files (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PathexecutableSource = openFileDialog1.FileName;
                    if (File.Exists("C:\\HomeSandBox" + "\\" + Path.GetFileName(PathexecutableSource)))
                    {
                        File.Delete("C:\\HomeSandBox" + "\\" + Path.GetFileName(PathexecutableSource));

                    }
                    File.Copy(PathexecutableSource, "C:\\HomeSandBox" + "\\" + Path.GetFileName(PathexecutableSource));
                    PathExecutable = "C:\\HomeSandBox" + "\\" + Path.GetFileName(PathexecutableSource);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        #endregion

        #region Chercher les permissions de l'exercie

        private void button1_Click(object sender, EventArgs e)
        {


            List<string> ListPermissions = new List<string>();

            openFileDialog1.InitialDirectory = "c:\\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName.ToString()))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ListPermissions.Add(line);
                        }
                    }
                    Permissions = PreparePermission(ListPermissions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur: ne peut pas lire sur le disque: " + ex.Message);
                }
            }

        }
        #endregion

        #region Creation des permission (Ipermission) à ajouter au PermissionSet

        public Dictionary<string, IPermission> PreparePermission(List<string> DemandPermissions)
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
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.Write, PathExecutable));
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            break;
                        case "READ":
                            Permissions.Add("Write", new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable));
                            Permissions.Add("Read", new FileIOPermission(FileIOPermissionAccess.Read, PathExecutable));
                            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, PathExecutable); break;
                        case "READWRITE":
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

               
                ExecuterCode(Permissions, PathExecutable);
            }
            catch (Exception ex)
            {
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                CodeAccessPermission.RevertAssert();
                Console.ReadLine();
                
            }
        }


        private string ExecuterCode(Dictionary<string, IPermission> Permissions, string PathExecute)
        {

            // Appel l'execution du code dans le sandbox 

            //string resul = ExecuterCode(Permissions, PathExecutable);    //  ExcuteCode(Permissions, PathExecutable);
           
            string resul = ObjDist.ExcuteCode(Permissions, PathExecutable);

            return "";
        }
    }


}
