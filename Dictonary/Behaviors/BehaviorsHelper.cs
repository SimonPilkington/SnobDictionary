using System;
using System.Windows;

namespace Dictonary.Behaviors
{
	public static class BehaviorsHelper
	{
		public static void ThrowOnInvalidDataContext<T>(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(e.NewValue is T))
				throw new InvalidOperationException("new datacontext is incompatible with an attached behavior");
		}
	}
}
