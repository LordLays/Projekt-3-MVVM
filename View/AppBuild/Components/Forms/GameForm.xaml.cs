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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KregulecApp.Model;
using KregulecApp.ViewModel;

namespace KregulecApp.View.AppBuild.Components.Forms
{
    /// <summary>
    /// Logika interakcji dla klasy GameForm.xaml
    /// </summary>
    public partial class GameForm : UserControl
    {
        private GameViewModel gameViewModel = new();
        public GameForm()
        {
            DataContext = gameViewModel;
            InitializeComponent();
            
        }
    }
}
