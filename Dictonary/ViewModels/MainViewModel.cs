using Dictonary.DataModel;
using Dictonary.Commands;
using System.Collections.ObjectModel;
using Dictonary.Services;
using Dictonary.Util;
using Dictonary.DataModel.Interfaces;
using System;
using System.Windows;
using System.ComponentModel;

namespace Dictonary.ViewModels
{
	public class MainViewModel : NotifyPropertyChangedBase
	{
        public ObservableCollection<IWordTreeViewItem> TreeViewItems { get; }
		public WordCategoryViewModel MainCategory { get; }

		public TreeViewDataService<IWordTreeViewItem> DataService { get; }
		public IDialogService DialogService { get; set; }
		
		public string CurrentWord
		{
			get
			{
				return DataService.CurrentWord;
			}
			set
			{
				if (value != DataService.CurrentWord)
				{
					DataService.CurrentWord = value;
					NotifyPropertyChanged();
				}
			}
		}

		private string _searchBoxString;
		public string SearchBoxString
		{
			get
			{
				return _searchBoxString;
			}
			set
			{
				if (value != _searchBoxString)
				{
					_searchBoxString = value;
					NotifyPropertyChanged();
				}
			}
		}
		
		public BasicCommand SaveWordTreeCommand { get; }
		public BasicCommand FindWordCommand { get; }
		public BasicCommand ViewClosingActionCommand { get; }

		public MainViewModel()
		{
			SaveWordTreeCommand = new BasicCommand(SaveWordTree);
			FindWordCommand = new BasicCommand(FindWord);
			ViewClosingActionCommand = new BasicCommand(WindowClosingAction);

			DataService = new TreeViewDataService<IWordTreeViewItem>();
			DataService.SelectedItemChanged += (o, e) => CurrentWord = DataService.SelectedItem.Text;

			MainCategory = new WordCategoryViewModel("Main", null, DataService, false);

			var serializer = new TreeViewHierarchyXmlSerializer(this);
			MainCategory.Children = new ObservableCollection<IWordTreeViewItem>(serializer.DeserializeXml());

			MainCategory.IsExpanded = true;
			MainCategory.StartRenameCommand.Enabled = false;

			TreeViewItems = new ObservableCollection<IWordTreeViewItem> { MainCategory };
		}
		
		private void SaveWordTree(object _)
		{
			var serializer = new TreeViewHierarchyXmlSerializer(this);
			serializer.SerializeToXml();

			DataService.DataAltered = false;
		}

		private void FindWord(object _)
		{
			CurrentWord = SearchBoxString;
		}

		private void WindowClosingAction(object cancelEventArgsObject)
		{
			var cancelEventArgs = cancelEventArgsObject as CancelEventArgs;
			
			if (cancelEventArgs == null)
				throw new ArgumentException(nameof(cancelEventArgsObject));

			if (!DataService.DataAltered)
				return;

			MessageBoxResult result = DialogService.ShowMessageBox("The word tree has changed. Save changes?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.None);

			switch (result)
			{
				case MessageBoxResult.Yes:
					SaveWordTree(null);
					break;
				case MessageBoxResult.Cancel:
					cancelEventArgs.Cancel = true;
					break;
			}
		}
	}
}
