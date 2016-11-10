using System.Windows;
using System.Windows.Controls;

namespace Dictonary
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
        }

		private void RenameTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (textBox.Visibility == Visibility.Visible)
			{
				textBox.SelectAll();
				textBox.Focus();
			}
		}
	}
}
