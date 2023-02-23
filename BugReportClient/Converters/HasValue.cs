using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BugReportClient.Converters;

public class HasValue : MarkupExtension, IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
            return !string.IsNullOrEmpty(str);
        else if (value is ICollection list)
            return list.Count > 0;
        else
            return value != default;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        this;

}