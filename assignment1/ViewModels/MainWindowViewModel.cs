using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;

namespace assignment1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public int X { get; }
        public int Y { get; }

        private string _values = string.Empty;
        public string Testik { get; } = "ASDASDASDAS";

        public string Values
        {
            get => _values;
            set => SetProperty(ref _values, value); // Triggers UI update automatically
        }

        public ICommand ToggleValueCommand { get; }

        public MainWindowViewModel()
        {
            // Read dimensions and values from file
            string[] lines = File.ReadAllLines("./Models/smile.b2img.txt");

            if (lines.Length < 2)
                throw new Exception("Invalid file format: expected at least two lines.");

            string[] splitWords = lines[0].Split(' ');
            if (splitWords.Length < 2)
                throw new Exception("Invalid file format: expected two integers in the first line.");

            X = Convert.ToInt32(splitWords[0]); // Number of rows
            Y = Convert.ToInt32(splitWords[1]); // Number of columns
            Values = lines[1]; // 1's and 0's representing the grid

            // Initialize the command to toggle value at a given index
            ToggleValueCommand = new RelayCommand(param =>
            {
                if (param is int index)
                {
                    ToggleValue(index);
                }
            });
        }

        private void ToggleValue(int index)
        {
            if (string.IsNullOrEmpty(Values) || index < 0 || index >= Values.Length)
                return;

            char[] chars = Values.ToCharArray();
            chars[index] = chars[index] == '0' ? '1' : '0';

            Values = new string(chars); // `SetProperty` will notify the UI
        }
    }
}
