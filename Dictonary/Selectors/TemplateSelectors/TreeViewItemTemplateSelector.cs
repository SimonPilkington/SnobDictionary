using Dictonary.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Dictonary.Selectors.TemplateSelectors
{
	public class TreeViewItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate WordDataTemplate { get; set; }
		public DataTemplate CategoryDataTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item is WordViewModel)
				return WordDataTemplate;

			if (item is WordCategoryViewModel)
				return CategoryDataTemplate;

			throw new ArgumentException(nameof(item));
		}
	}
}
