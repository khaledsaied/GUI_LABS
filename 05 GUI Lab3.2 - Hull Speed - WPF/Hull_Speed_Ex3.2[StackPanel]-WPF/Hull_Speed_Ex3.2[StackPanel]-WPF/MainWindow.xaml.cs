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

namespace Hull_Speed_Ex3._2_StackPanel__WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sailboat sb;
        
        public MainWindow()
        {
            InitializeComponent();
            sb = new Sailboat();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);
        }

        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.L:
                    {
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            FontSize += 2;
                            e.Handled = true;
                        }
                    }
                    break;
                case Key.S:
                    {
                        if ((Keyboard.Modifiers == ModifierKeys.Control) && FontSize >= 6)
                        {
                            FontSize -= 2;
                            e.Handled = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(nameBox);
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                sb.Name = nameBox.Text;
                sb.Length = double.Parse(lengthBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong format!!");
            }

            hullSpeed.Content = sb.Hullspeed();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("The name of the ship is " + sb.Name);
        }

        private void tbxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            sb.Name = nameBox.Text;
        }

        private void tbxLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lengthBox.Text.Trim() != "")
                try
                {
                    sb.Length = Double.Parse(lengthBox.Text);
                }
                catch
                {
                    MessageBox.Show("The length field must only contaion numbers");
                }
        }
    }
}
