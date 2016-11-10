using Dictonary.Behaviors.Interfaces;
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class DragBehavior : Behavior<FrameworkElement>
	{
		private bool _canStartDragAndDrop;

		public DragDropEffects AllowedEffects { get; set; }

		protected override void OnAttached()
		{
			base.OnAttached();

			if (AssociatedObject.DataContext is IDraggable)
			{
				AssociatedObject.MouseDown += MouseDown;
				AssociatedObject.MouseUp += MouseUp;
				AssociatedObject.MouseLeave += MouseLeave;
				AssociatedObject.DataContextChanged += BehaviorsHelper.ThrowOnInvalidDataContext<IDraggable>;
			}
			else
				throw new InvalidOperationException($"{nameof(AssociatedObject.DataContext)} must implement {nameof(IDraggable)}");
		}

		private void MouseDown(object sender, EventArgs e)
		{
			_canStartDragAndDrop = true;
		}

		private void MouseUp(object sender, EventArgs e)
		{
			_canStartDragAndDrop = false;
		}

		private void MouseLeave(object sender, EventArgs e)
		{
			if (_canStartDragAndDrop)
			{
				var draggableObject = AssociatedObject.DataContext as IDraggable;
				if (draggableObject.DragEnabled)
				{
					var data = new DataObject(draggableObject.Type, draggableObject);
					DragDrop.DoDragDrop(AssociatedObject, data, AllowedEffects);
				}

				_canStartDragAndDrop = false;
			}
		}
	}
}
