using System;
using System.IO;

namespace assignment1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  public int X { get; }
  public int Y { get; }
  public string values { get; }

  public MainWindowViewModel(){
    string[] lines = File.ReadAllLines("./Models/smile.b2img.txt");
    string[] splitWords = lines[0].Split(' ');
    X = Convert.ToInt32(splitWords[0]);
    Y = Convert.ToInt32(splitWords[1]);
    values = lines[1];
  }
}