using System;
using System.Collections.Generic;
using System.Windows;

namespace Dictonary.Behaviors.Interfaces
{
	public interface IDroppable
	{
		IReadOnlyCollection<Type> DroppableTypes { get; }

		DragDropEffects GetAllowedEffects(Type type);
		void Drop(object o);
	}
}
