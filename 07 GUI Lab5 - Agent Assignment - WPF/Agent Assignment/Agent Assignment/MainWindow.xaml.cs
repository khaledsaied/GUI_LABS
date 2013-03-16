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
using Win1;

namespace Agent_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>2
    public partial class MainWindow : Window 
    {
        private Agent agentAtrr = new Agent();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = agentAtrr;
            
        }

        private void NameId_TextChanged(object sender, TextChangedEventArgs e)
        {
           agentAtrr.ID = NameId.Text;
        }
    }
}
