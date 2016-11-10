using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class KeyPressReactionBehavior : Behavior<FrameworkElement>
	{
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(KeyPressReactionBehavior), new PropertyMetadata());

		public Key Key { get; set; }

		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.KeyUp += KeyUp;
		}

		private void KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key && Command?.CanExecute(null) == true)
				Command?.Execute(null);
		}
	}
}
