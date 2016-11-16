using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class WindowClosingActionBehavior: Behavior<Window>
	{
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(WindowClosingActionBehavior), new PropertyMetadata());
		
		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.Closing += WindowClosing;
		}

		private void WindowClosing(object sender, CancelEventArgs e)
		{
			if (Command?.CanExecute(null) != true)
			{
				e.Cancel = true;
				return;
			}
			
			Command?.Execute(e);
		}
	}
}
