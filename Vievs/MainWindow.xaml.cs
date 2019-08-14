﻿using System;
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
            this.DataContext = new VievModels.MainVievModel();
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
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(0);
                brd.BeginAnimation(Border.HeightProperty, da);
                IsToggle = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brd.Height = 0;
        }

        private void Btn_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
