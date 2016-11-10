using Dictonary.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Dictonary.Selectors.StyleSelectors
{
	public class TreeViewItemContainerStyleSelector : StyleSelector
	{
		public Style WordStyle { get; set; }
		public Style CategoryStyle { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			if (item is WordViewModel)
				return WordStyle;

			if (item is WordCategoryViewModel)
				return CategoryStyle;

			throw new ArgumentException(nameof(item));
		}
	}
}
