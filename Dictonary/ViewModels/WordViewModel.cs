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
						DataService.SelectedItem = this;
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

		public WordViewModel(string word, IWordTreeViewItem parent, TreeViewDataService<IWordTreeViewItem> dataService)
			: base(word, dataService)
		{
			if (parent == null)
				throw new ArgumentNullException(nameof(parent));

			Parent = parent;

			RemoveCommand = new BasicCommand(_ => Remove());
		}

		#region IDraggable
		public override Type Type => typeof(WordViewModel);
		#endregion

		#region IDroppable
		private static readonly HashSet<Type> _droppableTypes = new HashSet<Type>(new[] { typeof(WordViewModel), typeof(string) });
		public IReadOnlyCollection<Type> DroppableTypes => _droppableTypes;
		
		public DragDropEffects GetAllowedEffects(Type type)
		{
			if (type == typeof(WordViewModel))
				return DragDropEffects.Move;

			if (type == typeof(string))
				return DragDropEffects.Copy;

			return DragDropEffects.None;
		}

		public void Drop(object o)
		{
			var treeViewItem = o as IWordTreeViewItem;

			if (treeViewItem != null)
			{
				int myIndex = Parent.Children.IndexOf(this);
				Parent.Children.Insert(myIndex + 1, treeViewItem);
				treeViewItem.Parent = Parent;
			}

			var str = o as string;

			if (str != null)
			{
				int myIndex = Parent.Children.IndexOf(this);

				var newItem = new WordViewModel(str, Parent, DataService);

				Parent.Children.Insert(myIndex + 1, newItem);
				newItem.Parent = Parent;
			}
		}
		#endregion
	}
}
