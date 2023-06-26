using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using KregulecApp.View.AppBuild.Components.Forms;
using KregulecApp.View.AppBuild.Library;
using KregulecApp.View.ContentView;

namespace KregulecApp.View
{
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ButtonServices : INotifyPropertyChanged
    {
        private object currentControl;

        public object CurrentControl
        {
            get { return currentControl; }
            set
            {
                currentControl = value;
                OnPropertyChanged("CurrentControl");
            }
        }

        public ICommand showGameControl { get; }
        public ICommand showTagControl { get; }
        public ICommand showGameForm { get; }
        public ICommand showTagForm { get; }


        public ButtonServices()
        {
            showGameControl = new RelayCommand(ShowGameControl);
            showTagControl = new RelayCommand(ShowTagControl);
            showGameForm = new RelayCommand(ShowGameForm);
            showTagForm = new RelayCommand(ShowTagForm);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowGameControl()
        {
            CurrentControl = new GameControl();
        }

        private void ShowTagControl()
        {
            CurrentControl = new TagControl();
        }

        private void ShowGameForm()
        {
            CurrentControl = new GameForm();
        }
        private void ShowTagForm()
        {
            CurrentControl = new TagForm();
        }
    }
}
