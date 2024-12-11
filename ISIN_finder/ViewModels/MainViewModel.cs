using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISIN_finder.Models;
using System.Text;
using System.IO;

namespace ISIN_finder.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchInput = string.Empty;
        private string _selectedSearchType = "ISIN";
        private ObservableCollection<TradeInfo> _searchResults = new();
        private StringBuilder _consoleOutputBuilder;
        private string _consoleOutput = string.Empty;

        public MainViewModel()
        {
            SearchResults = new ObservableCollection<TradeInfo>();
            SearchCommand = new RelayCommand(ExecuteSearch);
            ClearCommand = new RelayCommand(ExecuteClear);
            _consoleOutputBuilder = new StringBuilder();
            Console.SetOut(new StringWriter(_consoleOutputBuilder));
            
            // Start a timer to update the console output
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) => UpdateConsoleOutput();
            timer.Start();
        }

        private void UpdateConsoleOutput()
        {
            ConsoleOutput = _consoleOutputBuilder.ToString();
        }

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSearchType
        {
            get => _selectedSearchType;
            set
            {
                _selectedSearchType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TradeInfo> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        public string ConsoleOutput
        {
            get => _consoleOutput;
            private set
            {
                if (_consoleOutput != value)
                {
                    _consoleOutput = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        private void ExecuteSearch()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Searching for {SelectedSearchType} values...");
            
            SearchResults.Clear();
            var inputs = SearchInput.Split(new[] { ',', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var input in inputs)
            {
                Console.WriteLine($"Processing input: {input}");
                var result = new TradeInfo
                {
                    ISIN = SelectedSearchType == "ISIN" ? input : $"ISIN_{input}",
                    ACID = SelectedSearchType == "ACID" ? input : $"ACID_{input}",
                    TradeId = SelectedSearchType == "TradeId" ? input : $"TRADE_{input}",
                    PositionLadder = (SelectedSearchType == "ACID" ? input : $"ACID_{input}") + "_positionladder"
                };
                SearchResults.Add(result);
                Console.WriteLine($"Added result: ISIN={result.ISIN}, ACID={result.ACID}, TradeId={result.TradeId}, PositionLadder={result.PositionLadder}");
            }

            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Search completed. Found {SearchResults.Count} results.");
        }

        private void ExecuteClear()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Clearing all data...");
            SearchInput = string.Empty;
            SearchResults.Clear();
            Console.WriteLine("Clear operation completed.");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();
    }
}
