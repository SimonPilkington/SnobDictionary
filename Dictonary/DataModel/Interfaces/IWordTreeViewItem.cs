using System.Collections.ObjectModel;

namespace Dictonary.DataModel.Interfaces
{
	public interface IWordTreeViewItem
	{
		string Text { get; }

		IWordTreeViewItem Parent { get; set; }
		ObservableCollection<IWordTreeViewItem> Children { get; }
	}
}
