using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace src.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _text = "";
    public string Text
    {
        get => _text;
        set
        {
            if (SetProperty(ref _text, value))
                UpdateNumbers();
        }
    }

    private string _lineNumbers = "1";
    public string LineNumbers
    {
        get => _lineNumbers;
        set => SetProperty(ref _lineNumbers, value);
    }

    private double _lineNumbersOffset;
    public double LineNumbersOffset
    {
        get => _lineNumbersOffset;
        set => SetProperty(ref _lineNumbersOffset, value);
    }

    private void UpdateNumbers()
    {
        var lineCount = string.IsNullOrEmpty(Text) ? 1 : Text.Split('\n').Length;
        LineNumbers = string.Join("\n", Enumerable.Range(1, lineCount));
    }
}