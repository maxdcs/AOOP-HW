using System;
using System.IO;
using System.Windows.Input;

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

        // Command expects a parameter (the index to toggle)
        public ICommand ToggleValueCommand { get; }

        public MainWindowViewModel()
        {
            // Read dimensions and values from file
            string[] lines = File.ReadAllLines("./Models/smile.b2img.txt");
            string[] splitWords = lines[0].Split(' ');
            X = Convert.ToInt32(splitWords[0]);
            Y = Convert.ToInt32(splitWords[1]);
            Values = lines[1];

            // Command uses the parameter (index) to toggle the appropriate character
            ToggleValueCommand = new RelayCommand(param =>
            {
                if (param is string s && int.TryParse(s, out int index))
                {
                    ToggleValue(index);
                }
                else if (param is int indexInt)
                {
                    ToggleValue(indexInt);
                }
            });
        }

        private void ToggleValue(int index)
        {
            if (string.IsNullOrEmpty(Values) || index < 0 || index >= Values.Length)
                return;
            char current = Values[index];
            char toggled = current == '0' ? '1' : '0';
            // Create a new char array from the string, update the character, then assign a new string
            char[] chars = Values.ToCharArray();
            chars[index] = toggled;
            Values = new string(chars);
        }
    }
}
