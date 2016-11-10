using System.Collections.Generic;

namespace Dictonary.ViewModel
{
	public interface ITreeViewItem
	{
		string Text { get; }
		IEnumerable<ITreeViewItem> SubItems { get; }
	}
}
