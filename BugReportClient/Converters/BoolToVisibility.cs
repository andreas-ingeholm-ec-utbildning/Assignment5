using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BugReportClient.Converters;

public class BoolToVisibility : MarkupExtension, IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is true
        ? Visibility.Visible
        : Visibility.Collapsed;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is Visibility.Visible;

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        this;

}
