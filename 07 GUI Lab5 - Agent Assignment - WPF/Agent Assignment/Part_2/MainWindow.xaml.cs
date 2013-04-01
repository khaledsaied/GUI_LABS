using System.Windows;
using Part_4;

namespace Part_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       Agents agentCollection = new Agents();

        public MainWindow()
        {
            InitializeComponent();
            agentCollection.Add(new Agent() { ID = "001", CodeName = "Nina", Speciality = "Assassination", Assignment = "UpperVolta" });
            agentCollection.Add(new Agent("007","Bo","adresse","spec","opgav"));
            agentCollection.Add(new Agent("911", "KS", "nyadresse", "speciale", "opgaverre"));
            lbxAgents.ItemsSource = agentCollection;
        }
    }
}
