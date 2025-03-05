using System.ComponentModel;
using Avalonia.Controls;
using assignment1.ViewModels;

namespace assignment1.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel; // Set the ViewModel as DataContext

            // Subscribe to property change notifications
            if (_viewModel != null)
            {
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }

            LoadButtons(); // Initial button load
        }

        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowViewModel.Values))
            {
                LoadButtons(); // Reload buttons when Values changes
            }
        }

        private void LoadButtons()
        {
            if (_viewModel == null || string.IsNullOrEmpty(_viewModel.Values)) return;

            ButtonContainer.Children.Clear(); // Clear existing buttons
            ButtonContainer.RowDefinitions.Clear();
            ButtonContainer.ColumnDefinitions.Clear();

            // Set the grid row and column definitions based on X and Y
            for (int row = 0; row < _viewModel.X; row++)
            {
                ButtonContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            for (int col = 0; col < _viewModel.Y; col++)
            {
                ButtonContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            // Dynamically create buttons and place them in the grid
            for (int i = 0; i < _viewModel.Values.Length; i++)
            {
                int row = i / _viewModel.Y; // Determine the row based on the index
                int col = i % _viewModel.Y; // Determine the column based on the index

                var button = new Button
                {
                    Content = _viewModel.Values[i].ToString(),
                    Margin = new Avalonia.Thickness(1),
                    Command = _viewModel.ToggleValueCommand,
                    CommandParameter = i // Pass the index as the parameter
                };

                // Set the button's row and column
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);

                ButtonContainer.Children.Add(button); // Add the button to the grid
            }
        }
    }
}
