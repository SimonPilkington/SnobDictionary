using System.Reflection;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class DisableWebBrowserDialogsBehavior : Behavior<WebBrowser>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			dynamic activeX = AssociatedObject.GetType().InvokeMember("ActiveXInstance",
					BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
					null, AssociatedObject, null);

			activeX.Silent = true;
		}
	}
}
