using Dictonary.Behaviors.Interfaces;
using Dictonary.DataModel;
using Dictonary.DataModel.Interfaces;
using Dictonary.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

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
					DataService.DataAltered = true;
				}
			}
		}

		public IWordTreeViewItem Parent { get; set; }


		public ICollectionView ChildrenView { get; private set; }

		private ObservableCollection<IWordTreeViewItem> _children;
		public virtual ObservableCollection<IWordTreeViewItem> Children
		{
			get
			{
				return _children;
			}
			set
			{
				if (!ReferenceEquals(value, _children))
				{
					if (value != null)
						value.CollectionChanged += _children_CollectionChanged;

					if (_children != null)
						_children.CollectionChanged -= _children_CollectionChanged;

					_children = value;
					ChildrenView = CollectionViewSource.GetDefaultView(_children);

					NotifyPropertyChanged();
					NotifyPropertyChanged(nameof(ChildrenView));
				}
			}
		}

		private void _children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			DataService.DataAltered = true;
		}
		#endregion

		public TreeViewDataService<IWordTreeViewItem> DataService
		{
			get;
		}

		public TreeViewItemViewModelBase(string header, TreeViewDataService<IWordTreeViewItem> dataService)
		{
			if (header == null)
				throw new ArgumentNullException(nameof(header));

			if (dataService == null)
				throw new ArgumentNullException(nameof(dataService));

			_header = header;
			DataService = dataService;
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
