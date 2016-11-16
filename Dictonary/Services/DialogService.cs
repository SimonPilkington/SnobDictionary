using System.Windows;

namespace Dictonary.Services
{
	public class DialogService : IDialogService
	{
		public MessageBoxResult ShowMessageBox(string messageBoxText)
		{
			return MessageBox.Show(messageBoxText);
		}

		public MessageBoxResult ShowMessageBox(string messageBoxText, string caption)
		{
			return MessageBox.Show(messageBoxText, caption);
		}

		public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button)
		{
			return MessageBox.Show(messageBoxText, caption, button);
		}

		public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
		{
			return MessageBox.Show(messageBoxText, caption, button, icon);
		}

		public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
		{
			return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult);
		}

		public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
		{
			return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
		}
	}
}
