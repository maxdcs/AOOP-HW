using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;

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
            set => SetProperty(ref _values, value);
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

            X = Convert.ToInt32(splitWords[0]); // Amount of lines
            Y = Convert.ToInt32(splitWords[1]); // Amount of columns
            Values = lines[1]; // 1's and 0's

            // Initialize the command with a lambda that passes the index to ToggleValue
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

            char current = Values[index];
            char toggled = current == '0' ? '1' : '0';

            // Update the character in the string
            char[] chars = Values.ToCharArray();
            chars[index] = toggled;
            Values = new string(chars);
        }
    }
}
