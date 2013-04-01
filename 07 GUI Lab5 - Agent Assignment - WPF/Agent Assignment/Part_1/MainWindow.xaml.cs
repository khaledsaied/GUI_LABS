using System.Windows;
using System.Windows.Controls;

namespace Part_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Agent agentAtrr = new Agent();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = agentAtrr;
        }

        private void tbxAssignment_TextChanged(object sender, TextChangedEventArgs e)
        {
            agentAtrr.Assignment = tbxAssignment.Text;
        }
    }
}
