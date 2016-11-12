using Dictonary.Behaviors.Interfaces;
using Dictonary.Commands;
using Dictonary.DataModel.Interfaces;
using Dictonary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Dictonary.ViewModels
{
	public class WordCategoryViewModel : TreeViewItemViewModelBase, IDroppable
	{
		private bool _isExpanded;
		public bool IsExpanded
		{
			get
			{
				return _isExpanded;
			}

			set
			{
				if (_isExpanded != value)
				{
					_isExpanded = value;
					NotifyPropertyChanged();
				}
			}
		}

		private bool _isRenaming;
		public bool IsRenaming
		{
			get
			{
				return _isRenaming;
			}

			set
			{
				if (_isRenaming != value)
				{
					_isRenaming = value;
					NotifyPropertyChanged();
				}
			}
		}

		public TreeViewDataService<IWordTreeViewItem> DataService
		{
			get;
		}

		public BasicCommand AddWordToCategoryCommand { get; }
		public BasicCommand AddSubcategoryCommand { get; }
		public BasicCommand StartRenameCommand { get; }
		public BasicCommand StopRenameCommand { get; }

		public WordCategoryViewModel(string header, IWordTreeViewItem parent, TreeViewDataService<IWordTreeViewItem> dataService, bool dragEnabled)
			: base (header)
		{
			Parent = parent;
			DragEnabled = dragEnabled;
			DataService = dataService;

			Children = new ObservableCollection<IWordTreeViewItem>();

			AddWordToCategoryCommand = new BasicCommand(AddWordToCategory);
			AddSubcategoryCommand = new BasicCommand(AddSubcategory);
			StartRenameCommand = new BasicCommand(StartRename);
			StopRenameCommand = new BasicCommand(StopRename);
		}

		public WordCategoryViewModel(string header, IWordTreeViewItem parent, TreeViewDataService<IWordTreeViewItem> dataService) : this (header, parent, dataService, true)
		{ }
		
		private void AddWordToCategory(object categoryObject)
		{
			var category = categoryObject as WordCategoryViewModel;

			if (category == null)
				throw new ArgumentException(nameof(categoryObject));

			Children.Add(new WordViewModel(DataService.CurrentWord, category, DataService));
			category.IsExpanded = true;
		}

		private void AddSubcategory(object categoryObject)
		{
			var category = categoryObject as WordCategoryViewModel;

			if (category == null)
				throw new ArgumentException(nameof(categoryObject));

			var newCategory = new WordCategoryViewModel("New category", category, DataService);

			Children.Add(newCategory);
			category.IsExpanded = true;
		}

		private void StartRename(object _)
		{
			IsRenaming = true;
		}

		private void StopRename(object _)
		{
			IsRenaming = false;
		}

		#region IDraggable

		public override Type Type => typeof(WordCategoryViewModel);
		public override bool DragEnabled { get; }

		#endregion

		#region IDroppable

		private static readonly HashSet<Type> _droppableTypes = new HashSet<Type>(new[] { typeof(WordCategoryViewModel), typeof(WordViewModel), typeof(string) });
		IReadOnlyCollection<Type> IDroppable.DroppableTypes => _droppableTypes;

		public void Drop(object o)
		{
			var treeViewItem = o as IWordTreeViewItem;

			if (treeViewItem != null)
			{
				Children.Insert(0, treeViewItem);
				treeViewItem.Parent = this;
			}

			var stringItem = o as string;

			if (stringItem != null)
			{
				var newWord = new WordViewModel(stringItem, this, DataService);

				Children.Insert(0, newWord);
				newWord.Parent = this;
			}
		}

		DragDropEffects IDroppable.GetAllowedEffects(Type type)
		{
			if (type == typeof(WordCategoryViewModel))
				return DragDropEffects.Move;

			if (type == typeof(WordViewModel))
				return DragDropEffects.Move;

			if (type == typeof(string))
				return DragDropEffects.Copy | DragDropEffects.Move;

			return DragDropEffects.None;
		}

		#endregion
	}
}
