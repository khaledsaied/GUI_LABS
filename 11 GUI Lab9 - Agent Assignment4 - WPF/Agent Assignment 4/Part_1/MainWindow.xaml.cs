using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace AgentAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes/Methods

        string filename = "";
        Agents agentCollection = new Agents();
        DispatcherTimer timer = new DispatcherTimer();
        Clock clock = new Clock();
        // Create a new XmlSerializer instance with the type of the Agent class
        private XmlSerializer SerializerObj = new XmlSerializer(typeof(Agent));
        // Create OpenFileDialog
        private Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        Random rnd;

        #endregion // Attributes/Methods

        public MainWindow()
        {
            InitializeComponent();
            DataContext = agentCollection;

            #region CLOCK

            spClock.DataContext = clock;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            #endregion // CLOCK

            #region DynamicRessources

            rnd = new Random(DateTime.Now.Millisecond); // for random Background Color

            #endregion // DynamicRessources

        }

        void Timer_Tick(object sender, EventArgs e)
        {
            clock.Update();
        }

        #region Command handlers

        private void CloseCommand_Executed(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenFileCommand_Executed(object sender, RoutedEventArgs e)
        {
            #region SettingUp OpenDialog
            // Set filter for file extension and default file extension
            dlg.Filter = "All Files (*.*)|*.*|Text documents (*.txt)|*.txt|XML Files (*.xml;*.xsl;*xsd;*dtd)|*.xml;*.xsl;*xsd;*dtd";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            #endregion //  SettingUp OpenDialog

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

            #endregion // Load the object
        }

        private void SaveAsCommand_Executed(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlgSave = new Microsoft.Win32.SaveFileDialog();
            //dlgSave.DefaultExt = ".txt";
            //dlg.Filter = "All Files (*.*)|*.*|Text documents (*.txt)|*.txt|XML Files (*.xml;*.xsl;*xsd;*dtd)|*.xml;*.xsl;*xsd;*dtd";

            if (dlgSave.ShowDialog() == true)
            {
                #region Save the object

                filename = dlgSave.FileName;

                if (dlgSave.FileName != "")
                {
                    filename = dlgSave.FileName;
                    SaveFileCommand_Executed(null, null);
                }
                else
                    MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);

                #endregion
            }
        }

        private void SaveFileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(Agents));
            TextWriter writer = new StreamWriter(filename);
            // Serialize all the agents.
            serializer.Serialize(writer, agentCollection);
            writer.Close();
        }

        private void SaveFileCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (filename != "") && (agentCollection.Count > 0);
        }

        private void NewFileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                agentCollection.Clear();
                filename = "";
            }
        }

        private void DeleteAgent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            agentCollection.RemoveAt(gridAgents.SelectedIndex);
        }

        private void DeleteAgent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = agentCollection.Count > 0 && gridAgents.SelectedIndex >= 0;
        }

        #endregion // Command handlers

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

        #region Sorting and filtering events

        private void Sort_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(gridAgents.ItemsSource) as ListCollectionView;
            var item = sender as ComboBox;

            string selectedSort = (string)(((ComboBoxItem)item.SelectedItem).Content);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(selectedSort, ListSortDirection.Ascending));
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;
            var filterItem = (string)(((ComboBoxItem)combobox.SelectedItem).Content);

            ListCollectionView view = CollectionViewSource.GetDefaultView(gridAgents.ItemsSource) as ListCollectionView;
            if (view != null)
            {
                var filter = new PropertyFilter(filterItem);
                if (filterItem.Equals("No Filter"))
                    view.Filter = null;
                else
                    view.Filter = new Predicate<object>(filter.FilterItem);
            }

        }

        #endregion // Sorting and filtering events
    }
}

