using System;

namespace Dictonary.Behaviors.Interfaces
{
	public interface IDraggable
	{
		Type Type { get; }
		int CurrentIndex { get; }
		bool DragEnabled { get; }

		void Remove();
	}
}
