using Dictonary.Commands;
using Dictonary.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using Dictonary.Behaviors.Interfaces;
using Dictonary.DataModel.Interfaces;

namespace Dictonary.ViewModels
{
	public class WordViewModel : TreeViewItemViewModelBase, IDroppable
	{
		private readonly TreeViewDataService<IWordTreeViewItem> _selectedItemService;
		
		private bool _isSelected;
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (_isSelected != value)
				{
					_isSelected = value;
					NotifyPropertyChanged();

					if (_isSelected)
						_selectedItemService.SelectedItem = this;
				}
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				throw new InvalidOperationException("property is read-only");
			}
		}

		public override ObservableCollection<IWordTreeViewItem> Children
		{
			get
			{
				return null;
			}

			set
			{
				throw new InvalidOperationException("trying to add children to leaf node");
			}
		}
			
		public BasicCommand RemoveCommand { get; }

		public WordViewModel(string word, IWordTreeViewItem parent, TreeViewDataService<IWordTreeViewItem> selectedItemService)
			: base(word)
		{
			Parent = parent;
			_selectedItemService = selectedItemService;

			RemoveCommand = new BasicCommand(_ => Remove());
		}

		#region IDraggable
		public override Type Type => typeof(WordViewModel);
		#endregion

		#region IDroppable
		private static readonly HashSet<Type> _droppableTypes = new HashSet<Type>(new[] { typeof(WordViewModel) });
		public IReadOnlyCollection<Type> DroppableTypes => _droppableTypes;
		
		public DragDropEffects GetAllowedEffects(Type type)
		{
			if (type == typeof(WordViewModel))
				return DragDropEffects.Move;

			return DragDropEffects.None;
		}

		public void Drop(object o)
		{
			var movedItem = o as IWordTreeViewItem;

			if (o != null)
			{
				int myIndex = Parent.Children.IndexOf(this);
				Parent.Children.Insert(myIndex + 1, movedItem);
				movedItem.Parent = Parent;
			}
		}
		#endregion
	}
}
