using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Exercise.ViewModels;

namespace Exercise.Views
{
    public partial class AltMainWindow : Window
    {
        public AltMainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
