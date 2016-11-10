using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class DoubleClickBehavior : Behavior<UIElement>
	{
		public MouseButton Button { get; set; }

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(DoubleClickBehavior), new PropertyMetadata());

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.MouseDown += DoubleClick;
		}

		private void DoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (Command?.CanExecute(null) == true
				&& e.ChangedButton == Button 
				&& e.ClickCount == 2)
			{
				Command?.Execute(null);
				e.Handled = true;
			}
		}
	}
}
