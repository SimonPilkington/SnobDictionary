using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class WebBrowserBindableSourceBehavior : Behavior<WebBrowser>
	{
		private bool _isAttached;

		public string BindableSource
		{
			get { return (string)GetValue(BindableSourceProperty); }
			set { SetValue(BindableSourceProperty, value); }
		}
		
		public static readonly DependencyProperty BindableSourceProperty =
			DependencyProperty.Register(nameof(BindableSource), typeof(string), typeof(WebBrowserBindableSourceBehavior), new PropertyMetadata(BindableSourceChanged));

		protected override void OnAttached()
		{
			base.OnAttached();
			_isAttached = true;
		}

		private static void BindableSourceChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var dependencyObject = (WebBrowserBindableSourceBehavior)sender;

			if (!dependencyObject._isAttached)
				return;

			WebBrowser associatedObject = dependencyObject.AssociatedObject;
			associatedObject.Source = new Uri((string)e.NewValue);
		}
	}
}
