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

namespace Part_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Agent> agentCollection = new List<Agent>();

        public MainWindow()
        {
            InitializeComponent();

            agentCollection.Add(new Agent("007", "Bo", "adresse", "spec", "opgav"));
            agentCollection.Add(new Agent("911", "KS", "nyadresse", "speciale", "opgaverre"));
            lbxAgents.ItemsSource = agentCollection;
            btnBack.Click += btnBack_Click;
            btnForward.Click += btnForward_Click;
            btnAdd.Click += btnAdd_Click;
        }

        void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAgents.SelectedIndex > 0)
                lbxAgents.SelectedIndex--;
        }

        void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAgents.SelectedIndex < lbxAgents.Items.Count - 1)
                lbxAgents.SelectedIndex++;
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            agentCollection.Add(new Agent("000", "***", "", "xxxxx", "..."));
            lbxAgents.Items.Refresh();
            lbxAgents.SelectedIndex = lbxAgents.Items.Count - 1;
        }
    }
}
