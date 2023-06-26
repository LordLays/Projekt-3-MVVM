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
using KregulecApp.ViewModel;

namespace KregulecApp.View.ContentView
{
    /// <summary>
    /// Logika interakcji dla klasy TagControl.xaml
    /// </summary>
    public partial class TagControl : UserControl
    {
        private TagViewModel _viewModel = new();
        public TagControl()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
