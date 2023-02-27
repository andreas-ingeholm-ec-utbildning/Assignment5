using System.Windows;
using IssuesClient.ViewModels;

namespace IssuesClient;

public partial class MainWindow : Window
{

    public MainWindow() =>
        InitializeComponent();

    public MainViewModel View { get; } = new();

}
