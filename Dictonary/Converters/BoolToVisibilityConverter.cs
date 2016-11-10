using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dictonary.Converters
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		public bool VisibleOnTrue { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(Visibility))
				throw new ArgumentException(nameof(targetType));

			bool bValue = VisibleOnTrue ? (bool)value : !((bool)value);

			return bValue ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
