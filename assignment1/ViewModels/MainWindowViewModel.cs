using System;
using System.IO;


namespace assignment1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public string[] lines;

    public string Item { get; }
    public string Item2 { get; }
    public string Testik { get; } = "blee";

    public MainWindowViewModel()
    {
        // Read the file inside the constructor to ensure proper file handling
        lines = File.ReadAllLines("./Models/smile.b2img.txt");
        if (lines.Length > 0)
        {
            Item = lines[0]; // Set the Item property to the first line
            Item2 = lines[1]; // Set the Item property to the first line
        }
        else
        {
            Item = "No lines found"; // Provide a default if file is empty
            Item2 = "No lines found"; // Provide a default if file is empty
        }
    }

}
