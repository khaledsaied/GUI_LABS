using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Xml.Serialization;
using Win1;

namespace Part_6_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filename = "";
        Agents agentCollection = new Agents();
        // Create a new XmlSerializer instance with the type of the Agent class
        private XmlSerializer SerializerObj = new XmlSerializer(typeof(Agent));
        // Create OpenFileDialog
        private Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        Random rnd;

        public MainWindow()
        {
            InitializeComponent();

            agentCollection.Add(new Agent("007", "Bo", "adresse", "spec", "opgav"));
            agentCollection.Add(new Agent("911", "KS", "nyadresse", "speciale", "opgaverre"));
            lbxAgents.ItemsSource = agentCollection;

            #region Events/Buttons

            btnBack.Click += btnBack_Click;
            btnForward.Click += btnForward_Click;
            btnAdd.Click += btnAdd_Click;
            btnMinus.Click += btnMinus_Click;

            #endregion // Events

            #region StatusBar

            dateTime.Text = DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString();
            agentCount.Text = agentCollection.Count.ToString();

            #endregion // StatusBar

            #region Commands

            // New
            CommandBinding cmdBindingNew = new CommandBinding(ApplicationCommands.New);
            cmdBindingNew.Executed += btnAdd_Click;
            CommandBindings.Add(cmdBindingNew);

            // Delete
            CommandBinding cmdBindingDelete = new CommandBinding(ApplicationCommands.Delete);
            cmdBindingDelete.Executed += btnMinus_Click;
            CommandBindings.Add(cmdBindingDelete);

            // Save
            CommandBinding cmdBindingSave = new CommandBinding(ApplicationCommands.Save);
            cmdBindingSave.Executed += SaveFile;
            CommandBindings.Add(cmdBindingSave);

            // SaveAs
            CommandBinding cmdBindingSaveAs = new CommandBinding(ApplicationCommands.SaveAs);
            cmdBindingSaveAs.Executed += SaveAs;
            CommandBindings.Add(cmdBindingSaveAs);

            // Open
            CommandBinding cmdBindingOpen = new CommandBinding(ApplicationCommands.Open);
            cmdBindingOpen.Executed += OpenFile;
            CommandBindings.Add(cmdBindingOpen);

            // Close
            CommandBinding cmdBindingClose = new CommandBinding(ApplicationCommands.Close);
            cmdBindingClose.Executed += Exit;
            CommandBindings.Add(cmdBindingClose);

            #endregion // Commands

            #region DynamicRessources

            rnd = new Random(DateTime.Now.Millisecond); // for random Background Color

            #endregion // DynamicRessources

        }

        #region Buttons/events

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
            agentCount.Text = agentCollection.Count.ToString();
        }

        void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbxAgents.Items.Count > 0 && lbxAgents.SelectedIndex <= lbxAgents.Items.Count)
                {
                    ((ObservableCollection<Agent>)lbxAgents.ItemsSource).Remove(
                        agentCollection[lbxAgents.SelectedIndex]);
                    lbxAgents.Items.Refresh();
                    lbxAgents.SelectedIndex = lbxAgents.Items.Count - 1;
                    agentCount.Text = agentCollection.Count.ToString();         // UPDATE StatusBar count on number of Agents
                }
                else if (lbxAgents.Items.Count == 0)
                    MessageBox.Show("No more agents in the list, please a new agent");
                else
                    MessageBox.Show("Select an Agent !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("You have not selected any agents!!!! \n\n More details:\n" + ex);
            }
        }

        #endregion // Buttons/events

        #region XMLSerializer

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            #region Save the object

            if (String.IsNullOrEmpty(tbxFileName.Text) && String.IsNullOrWhiteSpace(tbxFileName.Text))
                MessageBox.Show("You have not named your file ! Pleas enter a File name");
            else
            {
                MessageBoxResult key = MessageBox.Show(
                "Are you sure you want to save the file:\n '" + tbxFileName.Text + "'",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
                if (key == MessageBoxResult.Yes)
                {
                    //Create the file name
                    string file = tbxFileName.Text;

                    // Create a new file stream to write the serialized object to a file
                    TextWriter WriteFileStream = new StreamWriter(@file);
                    SerializerObj.Serialize(WriteFileStream, agentCollection);

                    // Cleanup
                    WriteFileStream.Close();
                }
                else
                {
                    return;
                    //Application.Current.Shutdown();
                }
            }

            #endregion
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            #region SettingUp OpenDialog
            // Set filter for file extension and default file extension
            //dlg.DefaultExt = ".txt";
            //dlg.Filter = "Text documents (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            #endregion //  SettingUp OpenDialog

            #region Open the textfile in NotePad

            //if (result == true)
            //    Process.Start(dlg.FileName);

            #endregion // open textfile

            #region Load the object

            filename = dlg.SafeFileName;
            Agents tempAgents = new Agents();

            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(Agents));
            try
            {
                TextReader reader = new StreamReader(filename);
                // Deserialize all the agents.
                tempAgents = (Agents)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // We have to insert the agents in the existing collection. If we just assign tempAgents to agents then the bindings to agents will brake!
            agentCollection.Clear();
            foreach (var agent in tempAgents)
                agentCollection.Add(agent);

            #region DEBUG OPEN
            //    if (result == true)
            //    {
            //        // chosen file name
            //        string file = dlg.SafeFileName;

            //        // Create a new file stream for reading the XML file
            //        FileStream ReadFileStream = new FileStream(@file, FileMode.Open, FileAccess.Read, FileShare.Read);

            //        // Load the object saved above by using the Deserialize function
            //        Agent LoadedObj = (Agent)SerializerObj.Deserialize(ReadFileStream);

            //        // Cleanup
            //        ReadFileStream.Close();

            //        // Test the new loaded object:
            //        MessageBox.Show("File name: " + file +
            //                        "\nID:\t\t" + LoadedObj.ID.ToString() +
            //                        " \nCodeName:\t" + LoadedObj.CodeName.ToString() +
            //                        "\nSpeciality:\t\t" + LoadedObj.Speciality.ToString() +
            //                        "\nAssignment:\t" + LoadedObj.Assignment.ToString());
            //    }
            #endregion //  DEBUG OPEN

            #endregion // Load the object

        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlgSave = new Microsoft.Win32.SaveFileDialog();
            //dlgSave.DefaultExt = ".txt";
            //dlgSave.Filter = "Text documents (.txt)|*.txt";

            if (dlgSave.ShowDialog() == true)
            {
                #region Save the object

                string filename = dlgSave.FileName;
                // Create a new file stream to write the serialized object to a file
                TextWriter WriteFileStream = new StreamWriter(@filename);
                SerializerObj.Serialize(WriteFileStream, agentCollection);

                // Cleanup
                WriteFileStream.Close();

                #endregion
            }
        }

        #endregion // XMLSerializer

        #region MenuEvents

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion // MenuEvents

        #region BackgroundColors

        private void RandColor_Click(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255));
            Brush brush = new SolidColorBrush(color);
            this.Resources["myBrush"] = brush;
        }

        private void RedColor_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.Red);
            this.Resources["myBrush"] = brush;
        }

        private void GreenColor_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.Green);
            this.Resources["myBrush"] = brush;
        }

        private void BlueColor_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.Blue);
            this.Resources["myBrush"] = brush;
        }

        #endregion // BackgroundColors


    }
}
