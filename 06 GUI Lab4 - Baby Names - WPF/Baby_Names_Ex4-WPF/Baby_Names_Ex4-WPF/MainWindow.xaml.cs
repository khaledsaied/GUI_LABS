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
using SIO = System.IO;

namespace Baby_Names_Ex4_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string str;
        private List<BabyName> babyNameListCollection;
        private string[,] babyNameArray = new string[11, 10]; // [year,rank,type]     

        public MainWindow()
        {
            InitializeComponent();
            for (int decade = 1900; decade < 2010; decade += 10)
                LstDecadeTopNames.Items.Add(decade);

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            LstDecadeTopNames.SelectionChanged += new SelectionChangedEventHandler(LstDecadeTopNames_SelectionChanged);
            btnSearch.Click += new RoutedEventHandler(Search);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string name = tbxName.Text;

            int i;
            //if (name == "")
            //    i = babyNameListCollection.Count + 1;
            //else
            //    i = babyNameListCollection.IndexOf(new BabyName(name + " 1 1 1 1 1 1 1 1 1 1 1"));

            // Alternative manual search 
            for (i = 0; i < babyNameListCollection.Count; ++i)
            {
               if (babyNameListCollection[i].Name == name)
                  break;
            }

            if (-1 < i && i < babyNameListCollection.Count)
            {
                tblkError.Text = "";
                BabyName theName = babyNameListCollection[i];
                tboxAvgRank.Text = theName.AverageRank().ToString();
                if (theName.Trend() > 0)
                    tboxTrend.Text = "More popular";
                else if (theName.Trend() == 0)
                    tboxTrend.Text = "Inconclusive";
                else
                    tboxTrend.Text = "Less popular";

                int rank;
                lstNameRanking.Items.Clear();
                for (int year = 1900; year < 2001; year += 10)
                {
                    rank = theName.Rank(year);
                    lstNameRanking.Items.Add(string.Format("{0:####}   {1:####}", year, rank));
                }
            }
            else
            {
                tblkError.Text = "Name not found!";
                tboxAvgRank.Text = "";
                tboxTrend.Text = "";
                lstNameRanking.Items.Clear();
            }
        }

        private void LstDecadeTopNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int decade;

            decade = (int)LstDecadeTopNames.SelectedItem;
            decade = (decade - 1900) / 10;
            TopTen.Items.Clear();
            for (int i = 1; i < 11; ++i)
            {
                TopTen.Items.Add(string.Format("{0,2} {1}", i, babyNameArray[decade, i - 1]));
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool fileFound = true;
            string file;
            file = SIO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Babynames.txt");
            try
            {
                this.babyNameListCollection = Utility.ReadBabyNameData(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error occured while starting Lab. 4.3:");
                Application.Current.Shutdown();
                fileFound = false;
            }

            if (fileFound)
                foreach (BabyName name in babyNameListCollection)
                {
                    for (int decade = 1900; decade < 2010; decade += 10)
                    {
                        int rank = name.Rank(decade);
                        int decadeIndex = (decade - 1900) / 10;
                        if (0 < rank && rank < 11)
                            if (babyNameArray[decadeIndex, rank - 1] == null)
                                babyNameArray[decadeIndex, rank - 1] = name.Name;
                            else
                                babyNameArray[decadeIndex, rank - 1] += " and " + name.Name;
                    }
                }
        }

        private void MenuItem_FileExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_SmallFont(object sender, RoutedEventArgs e)
        {
            this.FontSize = 8;
        }

        private void MenuItem_NormalFont(object sender, RoutedEventArgs e)
        {
            this.FontSize = 12;
        }

        private void MenuItem_LargeFont(object sender, RoutedEventArgs e)
        {
            this.FontSize = 18;
        }

        private void MenuItem_HugeFont(object sender, RoutedEventArgs e)
        {
            this.FontSize = 40;
        }
    }
}