using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Win1;


namespace Part_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Agent> agentCollection = new ObservableCollection<Agent>();
        // Create a new XmlSerializer instance with the type of the Agent class
        private XmlSerializer SerializerObj = new XmlSerializer(typeof(Agent));
        // Create OpenFileDialog
        private Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

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

            try
            {
                //Create the file name
                string file = fileName.Text + ".txt";

                // Create a new file stream to write the serialized object to a file
                TextWriter WriteFileStream = new StreamWriter(@file);
                SerializerObj.Serialize(WriteFileStream, agentCollection[lbxAgents.SelectedIndex]);

                // Cleanup
                WriteFileStream.Close();
            }
            catch (Exception ex)
            {
                string file = fileName.Text;
                MessageBox.Show("ERROR!! This filename: '"  + file + "'\n is not valid");
            }
           

            #endregion
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            try
            {
                #region SettingUp OpenDialog
                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                #endregion //  SettingUp OpenDialog

                #region Load the object

                if (result == true)
                {
                    // chosen file name
                    string file = dlg.SafeFileName;

                    // Create a new file stream for reading the XML file
                    FileStream ReadFileStream = new FileStream(@file, FileMode.Open, FileAccess.Read, FileShare.Read);

                    // Load the object saved above by using the Deserialize function
                    Agent LoadedObj = (Agent)SerializerObj.Deserialize(ReadFileStream);

                    // Cleanup
                    ReadFileStream.Close();

                    // Test the new loaded object:
                    MessageBox.Show("File name: " + file +
                                    "\nID:\t\t" + LoadedObj.ID.ToString() +
                                    " \nCodeName:\t" + LoadedObj.CodeName.ToString() +
                                    "\nSpeciality:\t\t" + LoadedObj.Speciality.ToString() +
                                    "\nAssignment:\t" + LoadedObj.Assignment.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("File '" + dlg.SafeFileName + "' does not exist!");
            }


                #endregion
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlgSave = new Microsoft.Win32.SaveFileDialog();
            dlgSave.DefaultExt = ".txt";
            dlgSave.Filter = "Text documents (.txt)|*.txt";

            if (dlgSave.ShowDialog() == true)
            {
                #region Save the object

                string filename = dlgSave.FileName;
                // Create a new file stream to write the serialized object to a file
                TextWriter WriteFileStream = new StreamWriter(@filename);
                SerializerObj.Serialize(WriteFileStream, agentCollection[lbxAgents.SelectedIndex]);

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



    }
}
