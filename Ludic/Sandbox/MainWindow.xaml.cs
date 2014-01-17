using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SandBoxDemostration
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> Enonce = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            List<String> tmp = new List<string>();
            tmp.Add("Tri de tableau");
            Enonce.Add("\n" + "L'exercice du jour consite à faire un tri d'un tableau " + 
                        "d'entiers d'une longueur de 1000 entrées générées aléatoirement" + "\n" +
                         "Au début de votre tri un compteur doit être déclenché" + "\n" +
                         "La meilleure solution sera celle qui fera le tri le plus rapidement possible !" );
            tmp.Add("Nombre Fibonacci");
            Enonce.Add("\n" + "On vous demande de coder une fonction qui indique si un nombre en entrée est un nombre Fibonicci ?  ");
            tmp.Add("Valeur abslue");
            Enonce.Add("Écrivez un code qui calcule la valeur absolue d'un réel");
            tmp.Add("Deux nombres amis");
            Enonce.Add("\n" + "Deux entiers naturels strictement positifis m et n sont dits nombres amis si seulement si :" + "\n" +
            "-la somme des diviseurs de m sauf lui-même est égale à n et "
            + "\n" + "- la somme des diviseurs de n sauf lui-même est égale à m  "+ "\n" +
            " Coder une fonction ou méthode qui donne une série de nombres amis.");
          
            this.ComboBox.ItemsSource = tmp;
            
        }

        public void Onclick(Object sender, EventArgs e)
        {
            this.TextBoxSortie.Text = "text a rajouter";
            Sandboxer sd = new Sandboxer();
            this.TextBoxSortie.Text = sd.init(this.ComboBox.SelectedIndex);

            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox tmp = (ComboBox)sender;
            this.TextBoxSortie.Text = "";
            this.TextBoxExec.Text = Enonce.ElementAt(tmp.SelectedIndex);
        }
    }
}
