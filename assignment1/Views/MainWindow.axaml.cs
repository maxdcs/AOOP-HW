using Avalonia.Controls;
using assignment1.ViewModels;

namespace assignment1.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Set the ViewModel as DataContext
            LoadButtons(); // Load the buttons dynamically based on the grid dimensions
        }

        private void LoadButtons()
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel == null || string.IsNullOrEmpty(viewModel.Values)) return;

            ButtonContainer.Children.Clear(); // Clear existing buttons

            // Set the grid row and column definitions based on X and Y
            for (int row = 0; row < viewModel.X; row++)
            {
                ButtonContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            for (int col = 0; col < viewModel.Y; col++)
            {
                ButtonContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            // Dynamically create buttons and place them in the grid
            for (int i = 0; i < viewModel.Values.Length; i++)
            {
                int row = i / viewModel.Y; // Determine the row based on the index
                int col = i % viewModel.Y; // Determine the column based on the index

                var button = new Button
                {
                    Content = viewModel.Values[i].ToString(),
                    Margin = new Avalonia.Thickness(1),
                    Command = viewModel.ToggleValueCommand,
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
