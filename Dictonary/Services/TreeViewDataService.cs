using System;

namespace Dictonary.Services
{
	public class TreeViewDataService<TSelectedItem>
	{
		private TSelectedItem _selectedItem;
		public TSelectedItem SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				if (!value.Equals(_selectedItem))
				{
					_selectedItem = value;
					SelectedItemChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private string _currentWord;
		public string CurrentWord
		{
			get
			{
				return _currentWord;
			}
			set
			{
				if (!value.Equals(_currentWord))
				{
					_currentWord = value;
					CurrentWordChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private bool _dataAltered;
		public bool DataAltered
		{
			get
			{
				return _dataAltered;
			}
			set
			{
				if (!value == _dataAltered)
				{
					_dataAltered = value;
					DataAlteredChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public event EventHandler SelectedItemChanged;
		public event EventHandler CurrentWordChanged;
		public event EventHandler DataAlteredChanged;
	}
}
