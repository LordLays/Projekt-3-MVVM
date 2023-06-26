using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using KregulecApp.Model;

namespace KregulecApp.ViewModel
{
    class TagViewModel : INotifyPropertyChanged
    {
        private Tags _selectedTag = new();
        private ObservableCollection<Tags> _tagsList;
        private RelayCommand<Tags> _addTagCommand;
        private RelayCommand<Tags> _updateTagCommand;
        private RelayCommand<Tags> _deleteTagCommand;
        private string _statusMessage;
        private string _updateTarget;

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        public TagViewModel()
        {
            _tagsList = new ObservableCollection<Tags>(GetTags());
        }

        public string Name
        {
            get => _selectedTag.Name;
            set
            {
                if (_selectedTag.Name != value)
                {
                    _selectedTag.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _selectedTag.Description;
            set
            {
                if (_selectedTag.Description != value)
                {
                    _selectedTag.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string UpdateTarget
        {
            get { return _updateTarget; }
            set
            {
                if (_updateTarget != value)
                {
                    _updateTarget = value;
                    OnPropertyChanged(nameof(_updateTarget));
                }
            }
        }
        public ObservableCollection<Tags> TagsList
        {
            get { return _tagsList; }
            set
            {
                _tagsList = value;
                OnPropertyChanged(nameof(TagsList));
            }
        }
        public ObservableCollection<Tags> GetTags()
        {
            using (var context = new KregulecDatabaseContext())
            {
                _tagsList = new ObservableCollection<Tags>(context.Tags.ToList());
                return _tagsList;
            }
        }

        public RelayCommand<Tags> AddTagCommand
        {
            get
            {
                if (_addTagCommand == null)
                    _addTagCommand = new RelayCommand<Tags>(AddTag);

                return _addTagCommand;
            }
        }
        public RelayCommand<Tags> UpdateTagCommand
        {
            get
            {
                if (_updateTagCommand == null)
                    _updateTagCommand = new RelayCommand<Tags>(UpdateTag);

                return _updateTagCommand;
            }
        }
        public RelayCommand<Tags> DeleteTagCommand
        {
            get
            {
                if (_deleteTagCommand == null)
                    _deleteTagCommand = new RelayCommand<Tags>(DeleteTag);

                return _deleteTagCommand;
            }
        }
        public void AddTag(Tags tag)
        {
            using (var context = new KregulecDatabaseContext())
            {
                tag = _selectedTag;
                context.Tags.Add(tag);
                int affectedRows = context.SaveChanges();
                _tagsList.Add(tag);


                if (affectedRows > 0)
                {
                    _tagsList.Add(tag);
                    StatusMessage = "Tag został prawidłowo dodany.";
                }
                else
                {
                    StatusMessage = "Nie udało się dodać tagu.";
                }
            }
        }
        public void UpdateTag(Tags tag)
        {
            using (var context = new KregulecDatabaseContext())
            {
                tag = _selectedTag;
                string target = _updateTarget;
                var existingGame = context.Tags.FirstOrDefault(e => target == e.Name);
                if (existingGame != null)
                {
                    context.Tags.Remove(existingGame);
                    context.Tags.Add(tag);
                    context.SaveChanges();
                    _tagsList.Remove(existingGame);
                    _tagsList.Add(tag);
                    StatusMessage = "Gra została zaktualizowana.";
                }
            }
        }

        public void DeleteTag(Tags tag)
        {
            tag = _selectedTag;
            using (var context = new KregulecDatabaseContext())
            {
                var existingTag = _tagsList.FirstOrDefault(e => e.Name == tag.Name);
                if (existingTag != null)
                {
                    context.Tags.Remove(existingTag);
                    context.SaveChanges();
                    _tagsList.Remove(existingTag);
                    StatusMessage = "Gra została usunięta.";
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
