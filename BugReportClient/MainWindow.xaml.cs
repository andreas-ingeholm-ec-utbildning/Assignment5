using BugReportClient.ViewModels;
using Wpf.Ui.Controls;

namespace BugReportClient;

public partial class MainWindow : UiWindow
{

    public MainWindow() =>
        InitializeComponent();

    public MainViewModel MainView { get; } = new MainViewModel();

}
