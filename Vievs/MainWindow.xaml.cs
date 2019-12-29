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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KOLHOZ_Marker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new VievModels.MainVievModel(this);
            
        }

        private bool IsToggle;

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (!IsToggle)
            {
                da.To = 90;
                da.Duration = TimeSpan.FromSeconds(0);
                brd.BeginAnimation(Border.HeightProperty, da);
                IsToggle = true;
                (DataContext as VievModels.MainVievModel).IsFiltering = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(0);
                brd.BeginAnimation(Border.HeightProperty, da);
                IsToggle = false;
                (DataContext as VievModels.MainVievModel).IsFiltering = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brd.Height = 0;
        }

    
        public void OnContextMenuOpened(object sender, RoutedEventArgs args)
        {
            (sender as ContextMenu).DataContext = this.DataContext;

            foreach (var item in (sender as ContextMenu).Items)
            {
                if (item is MenuItem)
                {
                    //set the command target to whatever you like here
                    (item as MenuItem).CommandParameter = dc;
                }
            }
        }

        string dc;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dc = ((sender as Button).DataContext as KOLHOZ_Marker.Models.MarkModel).ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
