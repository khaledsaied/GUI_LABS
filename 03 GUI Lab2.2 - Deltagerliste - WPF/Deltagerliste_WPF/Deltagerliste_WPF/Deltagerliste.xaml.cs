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
using System.Windows.Shapes;
using System.IO;

namespace Deltagerliste_WPF
{
    /// <summary>
    /// Interaction logic for Deltagerliste.xaml
    /// </summary>
    public partial class Deltagerliste : Window
    {
        public Deltagerliste()
        {
            InitializeComponent();
        }

        public string[] tokens;
        public string str, listString;
        public char[] separators = { ';' };

        private void listbox_Loaded(object sender, RoutedEventArgs e)
        {
                FileStream fs = new FileStream(@"deltagerliste.csv", FileMode.Open);
                StreamReader s = new StreamReader(fs, Encoding.Default);

                if (s != null)
                {
                    while (!s.EndOfStream)
                    {
                        str = s.ReadLine();
                        tokens = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        listString = tokens[0] + tokens[1] + tokens[2] + tokens[3];

                        fornavn.Items.Add(tokens[0]);
                        efternavn.Items.Add(tokens[1]);
                        brugernavn.Items.Add(tokens[2]);
                        email.Items.Add(tokens[3]);
                    }
                }
         }
     }
}
