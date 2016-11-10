using Dictonary.Behaviors.Interfaces;
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Dictonary.Behaviors
{
	public class DropBehavior : Behavior<FrameworkElement>
	{
		public DragDropEffects DropEffect { get; set; }

		protected override void OnAttached()
		{
			base.OnAttached();

			if (AssociatedObject.DataContext is IDroppable)
			{
				AssociatedObject.AllowDrop = true;
				AssociatedObject.DragEnter += DragEnter;
				AssociatedObject.DragOver += DragOver;
				AssociatedObject.Drop += DropEvent;
				AssociatedObject.DataContextChanged += BehaviorsHelper.ThrowOnInvalidDataContext<IDroppable>;
			}
			else
				throw new InvalidOperationException($"{nameof(AssociatedObject.DataContext)} must implement {nameof(IDroppable)}");
		}

		private void DragOver(object sender, DragEventArgs e)
		{
			SetEffect(e);
			e.Handled = true;
		}

		private void DragEnter(object sender, DragEventArgs e)
		{
			SetEffect(e);
			e.Handled = true;
		}

		private void DropEvent(object sender, DragEventArgs e)
		{
			var dropTarget = AssociatedObject.DataContext as IDroppable;

			Type dropType;
			var dropData = GetDropData(e.Data, dropTarget, out dropType);

			if (dropData != null)
			{
				if (dropData is IDraggable && (e.Effects & DragDropEffects.Copy) == DragDropEffects.None)
					(dropData as IDraggable).Remove();

				dropTarget.Drop(dropData);
				e.Handled = true;
			}
		}

		private void SetEffect(DragEventArgs e)
		{
			var dropTarget = AssociatedObject.DataContext as IDroppable;

			Type dropType;
			var dropData = GetDropData(e.Data, dropTarget, out dropType);

			e.Effects = DragDropEffects.None;

			if (dropData != null)
			{
				if (dropData != AssociatedObject.DataContext)
				{
					DragDropEffects possibleEffects = dropTarget.GetAllowedEffects(dropType);

					if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
						e.Effects = possibleEffects & DragDropEffects.Copy;
					else
						e.Effects = possibleEffects & (DragDropEffects.Move | DragDropEffects.Copy);
				}
			}
		}

		private static object GetDropData(IDataObject data, IDroppable dropTarget, out Type dataType)
		{
			foreach (var type in dropTarget.DroppableTypes)
			{
				if (data.GetDataPresent(type))
				{
					dataType = type; 
					return data.GetData(type);
				}
			}

			dataType = null;
			return null;
		}
	}
}
