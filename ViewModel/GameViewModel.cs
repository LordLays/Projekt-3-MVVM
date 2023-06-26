using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using KregulecApp.Model;
using KregulecApp.View;

namespace KregulecApp.ViewModel
{
    class GameViewModel : INotifyPropertyChanged
    {
        private GameCatalog _selectedGame = new();
        private ObservableCollection<GameCatalog> _gameList;
        private RelayCommand<GameCatalog> _addGameCommand;
        private RelayCommand<GameCatalog> _updateGameCommand;
        private RelayCommand<GameCatalog> _deleteGameCommand;
        private string _statusMessage;
        private string _updateTarget;

        public GameViewModel()
        {
            _gameList = new ObservableCollection<GameCatalog>(GetGameCatalogs());
        }
        public ObservableCollection<GameCatalog> GameCatalogs
        {
            get { return _gameList; }
            set
            {
                _gameList = value;
                OnPropertyChanged(nameof(GameCatalogs));
            }
        }

        public string Title
        {
            get => _selectedGame.Title;
            set
            {
                _selectedGame.Title = value;
                    OnPropertyChanged();
            }
        }

        public string Publisher
        {
            get => _selectedGame.Publisher;
            set
            {
                _selectedGame.Publisher = value;
                OnPropertyChanged();
            }
        }

        public DateTime ReleaseDate
        {
            get => _selectedGame.ReleaseDate;
            set
            {
                _selectedGame.ReleaseDate = value;
                OnPropertyChanged();
            }
        }

        public int Players
        {
            get => _selectedGame.Players;
            set
            {
                _selectedGame.Players = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan PlayTime
        {
            get => _selectedGame.PlayTime;
            set
            {
                _selectedGame.PlayTime = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => _selectedGame.Age;
            set
            {
                _selectedGame.Age = value;
                OnPropertyChanged();
            }
        }

        public string Language
        {
            get => _selectedGame.Language;
            set
            {
                _selectedGame.Language = value;
                OnPropertyChanged();
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

        public RelayCommand<GameCatalog> AddGameCommand
        {
            get
            {
                if (_addGameCommand == null)
                    _addGameCommand = new RelayCommand<GameCatalog>(AddGame);

                return _addGameCommand;
            }
        }

        public RelayCommand<GameCatalog> UpdateGameCommand
        {
            get
            {
                if (_updateGameCommand == null)
                    _updateGameCommand = new RelayCommand<GameCatalog>(UpdateGame);

                return _updateGameCommand;
            }
        }

        public RelayCommand<GameCatalog> DeleteGameCommand
        {
            get
            {
                if (_deleteGameCommand == null)
                    _deleteGameCommand = new RelayCommand<GameCatalog>(DeleteGame);

                return _deleteGameCommand;
            }
        }

        public ObservableCollection<GameCatalog> GetGameCatalogs() 
        {
            using (var context = new KregulecDatabaseContext())
            {
                _gameList = new ObservableCollection<GameCatalog>(context.GameCatalogs.ToList());
                return _gameList;
            }
        }

    public void AddGame(GameCatalog game)
        {
            using (var context = new KregulecDatabaseContext())
            {
                game = _selectedGame;
                context.GameCatalogs.Add(game);
                int affectedRows = context.SaveChanges();
                _gameList.Add(game);
                

                if (affectedRows > 0)
                {
                    _gameList.Add(game);
                    StatusMessage = "Gra została prawidłowo dodana.";
                }
                else
                {
                    StatusMessage = "Nie udało się dodać gry.";
                }
            }
        }
        public void UpdateGame(GameCatalog game)
        {
            using (var context = new KregulecDatabaseContext())
            {
                game = _selectedGame;
                string target = _updateTarget;
                var existingGame = context.GameCatalogs.FirstOrDefault(e => target == e.Title);
                if (existingGame != null)
                {
                    context.GameCatalogs.Remove(existingGame);
                    context.GameCatalogs.Add(game);
                    context.SaveChanges();
                    _gameList.Remove(existingGame);
                    _gameList.Add(game);
                    StatusMessage = "Gra została zaktualizowana.";
                }
            }
        }

        public void DeleteGame(GameCatalog game)
        {
            game = _selectedGame;
            using (var context = new KregulecDatabaseContext())
            {
                var existingGame = _gameList.FirstOrDefault(e => e.Title == game.Title);
                if (existingGame != null)
                {
                    context.GameCatalogs.Remove(existingGame);
                    context.SaveChanges();
                    _gameList.Remove(existingGame);
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
