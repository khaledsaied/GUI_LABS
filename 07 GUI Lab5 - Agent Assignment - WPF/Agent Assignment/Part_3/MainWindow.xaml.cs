using System.Windows;
using Part_4;

namespace Part_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region ButtonEvents

            btnBack.Click += btnBack_Click;
            btnForward.Click += btnForward_Click;
            btnAdd.Click += btnAdd_Click;

            #endregion //ButtonEvents
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
            Agents agents = (Agents)this.FindResource("agents");
            agents.Add(new Agent("000", "***", "", "xxxxx", "..."));
            lbxAgents.SelectedIndex = lbxAgents.Items.Count - 1;
            NameId.Focus();
        }
    }
}
