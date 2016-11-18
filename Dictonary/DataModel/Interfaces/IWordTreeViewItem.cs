using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dictonary.DataModel.Interfaces
{
	public interface IWordTreeViewItem
	{
		string Text { get; }

		IWordTreeViewItem Parent { get; set; }
		ObservableCollection<IWordTreeViewItem> Children { get; }
		ICollectionView ChildrenView { get; }
	}
}
