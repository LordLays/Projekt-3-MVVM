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

namespace KregulecApp.View.AppBuild.Components.Forms
{
    /// <summary>
    /// Logika interakcji dla klasy TagForm.xaml
    /// </summary>
    public partial class TagForm : UserControl
    {
        private TagViewModel tagViewModel = new();
        public TagForm()
        {
            DataContext = tagViewModel;
            InitializeComponent();
        }
    }
}
