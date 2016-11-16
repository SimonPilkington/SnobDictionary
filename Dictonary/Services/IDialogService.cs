using System.Windows;

namespace Dictonary.Services
{
	public interface IDialogService
	{
		MessageBoxResult ShowMessageBox(string messageBoxText);
		MessageBoxResult ShowMessageBox(string messageBoxText, string caption);
		MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button);
		MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
		MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult);
		MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options);
	}
}
