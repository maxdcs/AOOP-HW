using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace assignment1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private string _values = string.Empty;
        public string DimensionsText => X > 0 && Y > 0 ? $"size: {X}x{Y}" : "No file loaded";

        public string Values
        {
            get => _values;
            set => SetProperty(ref _values, value);
        }

        private string _inputText = string.Empty;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        private string _outputFileName = string.Empty;
        public string OutputFileName
        {
            get => _outputFileName;
            set => SetProperty(ref _outputFileName, value);
        }

        public ICommand ToggleValueCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand LoadFileCommand { get; }
        public ICommand FlipValuesCommand { get; } // New flip command

        public MainWindowViewModel()
        {
            // Initialize commands
            ToggleValueCommand = new RelayCommand(param =>
            {
                if (param is int index)
                {
                    ToggleValue(index);
                }
            });

            SaveFileCommand = new RelayCommand(_ =>
            {
                if (string.IsNullOrWhiteSpace(InputText))
                {
                    Console.WriteLine("No filename provided.");
                    return;
                }

                string directoryPath = "./Models";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, $"{InputText}.b2img.txt");

                string[] outputContent = [$"{X} {Y}", Values];
                File.WriteAllLines(filePath, outputContent);

                OutputFileName = InputText; // Update the UI
                Console.WriteLine($"File saved as: {filePath}");
            });

            LoadFileCommand = new RelayCommand(_ => LoadFile());

            // Initialize Flip command
            FlipValuesCommand = new RelayCommand(_ => FlipValues());
        }

        private void LoadFile()
        {
            string filePath = "./Models/smile.b2img.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length < 2)
            {
                Console.WriteLine("Invalid file format.");
                return;
            }

            string[] splitWords = lines[0].Split(' ');
            if (splitWords.Length < 2)
            {
                Console.WriteLine("Invalid file format.");
                return;
            }

            X = Convert.ToInt32(splitWords[0]);
            Y = Convert.ToInt32(splitWords[1]);
            Values = lines[1];

            OnPropertyChanged(nameof(DimensionsText));
        }

        private void ToggleValue(int index)
        {
            if (string.IsNullOrEmpty(Values) || index < 0 || index >= Values.Length)
                return;

            char[] chars = Values.ToCharArray();
            chars[index] = chars[index] == '0' ? '1' : '0';

            Values = new string(chars);
        }

        private void FlipValues()
        {
            if (!string.IsNullOrEmpty(Values))
            {
                Values = new string(Values.Reverse().ToArray());
            }
        }
    }
}