using Avalonia.Controls;
using Avalonia.VisualTree;
using System.Linq;
using Avalonia.Threading;

namespace src.Views;

public partial class MainWindow : Window
{
    private ScrollViewer? _textBoxScrollViewer;
    private bool _isUpdatingScroll = false;

    public MainWindow()
    {
        InitializeComponent();
        
        // Use Dispatcher to ensure the visual tree is fully loaded
        Dispatcher.UIThread.Post(InitializeScrollSynchronization, DispatcherPriority.Loaded);
    }
    
    private void InitializeScrollSynchronization()
    {
        // Find the internal ScrollViewer of the TextBox
        _textBoxScrollViewer = MainTextBox.GetVisualDescendants()
            .OfType<ScrollViewer>()
            .FirstOrDefault();
            
        if (_textBoxScrollViewer != null)
        {
            // Subscribe to scroll changes in the TextBox
            _textBoxScrollViewer.ScrollChanged += OnTextBoxScrollChanged;
            
            // Also subscribe to the line numbers ScrollViewer to prevent feedback loops
            LineNumbersScroll.ScrollChanged += OnLineNumbersScrollChanged;
        }
    }
    
    private void OnTextBoxScrollChanged(object? sender, ScrollChangedEventArgs e)
    {
        if (_isUpdatingScroll) return;
        
        _isUpdatingScroll = true;
        
        // Synchronize the line numbers ScrollViewer with the TextBox ScrollViewer
        LineNumbersScroll.Offset = new Avalonia.Vector(0, _textBoxScrollViewer!.Offset.Y);
        
        _isUpdatingScroll = false;
    }
    
    private void OnLineNumbersScrollChanged(object? sender, ScrollChangedEventArgs e)
    {
        // This prevents feedback loops but generally shouldn't be triggered
        // since the line numbers ScrollViewer is set to IsHitTestVisible="False"
    }
}
