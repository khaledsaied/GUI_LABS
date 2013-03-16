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

namespace Hull_Speed_Ex3._1_Environment__WPF
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] logicalDrives = Environment.GetLogicalDrives();
            //string currentDirectory = Environment.CurrentDirectory;
            txtBlock.Inlines.Add("Folder for personal documents: " + 
                Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            txtBlock.Inlines.Add("\nFolder for application specific user settings: " + 
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            txtBlock.Inlines.Add("\nSystem folder: " + Environment.GetFolderPath(Environment.SpecialFolder.System));
            txtBlock.Inlines.Add("\n\nCurrent directory: " + Environment.CurrentDirectory);

            //txtBlock.Inlines.Add("\nLogical drives: " + logicalDrives[0]+logicalDrives[1]+logicalDrives[2]+logicalDrives[3]);
            string str = "\nLogical drives: ";
            foreach (string s in logicalDrives)
                str += s.Substring(0, 2) + " ";
            txtBlock.Inlines.Add(str);
            txtBlock.Inlines.Add("\n\nMachine name: " + Environment.MachineName);
            txtBlock.Inlines.Add("\nUser name: " + Environment.UserName);
            txtBlock.Inlines.Add(new Bold(new Run("\n\nStackTrace:\n")));
            txtBlock.Inlines.Add(Environment.StackTrace);
            //txtBlock.Inlines.Add(new LineBreak());
            
        }

    }
}
