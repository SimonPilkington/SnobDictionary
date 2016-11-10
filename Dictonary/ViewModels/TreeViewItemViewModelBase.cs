using Dictonary.Behaviors.Interfaces;
using Dictonary.DataModel;
using Dictonary.DataModel.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace Dictonary.ViewModels
{
	public abstract class TreeViewItemViewModelBase : NotifyPropertyChangedBase, IWordTreeViewItem, IDraggable
	{
		#region IWordTreeViewItem
		private string _header;
		public virtual string Text
		{
			get
			{
				return _header;
			}

			set
			{
				if (_header != value)
				{
					_header = value;
					NotifyPropertyChanged();
				}
			}
		}

		public IWordTreeViewItem Parent { get; set; }

		public virtual ObservableCollection<IWordTreeViewItem> Children { get; set; }
		#endregion

		public TreeViewItemViewModelBase(string header)
		{
			_header = header;
		}

		#region IDraggable
		public abstract Type Type { get; }
		public virtual bool DragEnabled => true;
		public int CurrentIndex => Parent.Children.IndexOf(this);

		public void Remove()
		{
			Parent.Children.Remove(this);
		}
		#endregion
	}
}
