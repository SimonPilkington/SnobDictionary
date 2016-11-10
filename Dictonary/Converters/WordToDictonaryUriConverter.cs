using System;
using System.Globalization;
using System.Windows.Data;

namespace Dictonary.Converters
{
	public class WordToDictonaryUriConverter : IValueConverter
	{
		private Uri _baseUri;
		public string BaseUri
		{
			set { _baseUri = new Uri(value); }
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(string))
				throw new ArgumentException(nameof(targetType));

			var stringValue = value as string;

			Uri wordUri = new Uri(_baseUri, stringValue);

			return wordUri.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
