using System;
using System.IO;
using System.Windows.Input;


namespace assignment1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  public int X { get; }
  public int Y { get; }

  private string _values = string.Empty;
  public string Values
  {
    get => _values;
    set => SetProperty(ref _values, value);
  }
  public ICommand ToggleValueCommand { get; }

  public MainWindowViewModel()
  {
    string[] lines = File.ReadAllLines("./Models/smile.b2img.txt");
    string[] splitWords = lines[0].Split(' ');
    X = Convert.ToInt32(splitWords[0]);
    Y = Convert.ToInt32(splitWords[1]);
    Values = lines[1];
    ToggleValueCommand = new RelayCommand(_ => ToggleValue());
  }

  private void ToggleValue()
  {
    if (!string.IsNullOrEmpty(Values))
    {
      char firstChar = Values[0];
      char toggled = firstChar == '0' ? '1' : '0';
      Values = toggled + (Values.Length > 1 ? Values.Substring(1) : "");
    }
  }

}