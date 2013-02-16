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
using System.IO;


namespace deltagerliste_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string[] tokens;
        public string str, listString;
        public char[] separators = { ';' };

        private void ListBox_Loaded_1(object sender, RoutedEventArgs e)
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
                    listbox.Items.Add(listString);
                }
            }
        }

      
    }
}
