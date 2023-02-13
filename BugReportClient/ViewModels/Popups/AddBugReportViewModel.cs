using BugReportClient.Models;

namespace BugReportClient.ViewModels.Popups;

public partial class AddBugReportViewModel : PopupViewModel
{

    public BugReport Report { get; } = new();

}
