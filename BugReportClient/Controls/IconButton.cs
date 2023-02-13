using Wpf.Ui.Controls;

namespace BugReportClient.Controls;

/// <summary>Wpf.Ui.Controls.Button cannot be styled for some reason, so this is a work-around for that.</summary>
public class IconButton : Button
{

    public IconButton()
    {
        FontSize = 26;
        Padding = new(12);
        Margin = new(6);
    }

}
