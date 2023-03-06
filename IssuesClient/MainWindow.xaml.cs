using System.Windows;
using IssuesClient.ViewModels;
using Wpf.Ui.Controls;

namespace IssuesClient;

public partial class MainWindow : UiWindow
{

    public MainWindow() =>
        InitializeComponent();

    public MainViewModel View { get; } = new();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }
}
