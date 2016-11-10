using Dictonary.DataModel.Interfaces;
using Dictonary.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Dictonary.Util
{
	public class TreeViewHierarchyXmlSerializer
	{
		private const string WordNode = "Word";
		private const string CategoryNode = "Category";
		private const string RootNode = "TreeView";
		private const string TextAttribute = "Text";

		private MainViewModel _viewModel;

		public TreeViewHierarchyXmlSerializer(MainViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public void SerializeToXml()
		{
			var xmlDocument = new XmlDocument();

			xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));

			var rootNode = xmlDocument.CreateElement(RootNode);

			xmlDocument.AppendChild(rootNode);
			AddAllItemsToTree(xmlDocument, rootNode, _viewModel.TreeViewItems);

			string destination = Properties.Settings.Default.WordTreeFilePath;
			Directory.CreateDirectory(Path.GetDirectoryName(destination));

			xmlDocument.Save(destination);
		}

		public IEnumerable<IWordTreeViewItem> DeserializeXml()
		{
			Stream xmlStream = null;
			try
			{
				xmlStream = File.OpenRead(Properties.Settings.Default.WordTreeFilePath);
				return DeserializeXml(xmlStream);
			}
			catch (Exception x) when (x is DirectoryNotFoundException || x is FileNotFoundException)
			{
				return Enumerable.Empty<IWordTreeViewItem>();
			}
			finally
			{
				xmlStream?.Dispose();
			}
		}

		public IEnumerable<IWordTreeViewItem> DeserializeXml(Stream xmlStream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlStream);

			var rootNode = xmlDocument.SelectSingleNode(RootNode);
			return CreateItemsFromXml(rootNode, _viewModel.MainCategory);
		}

		private IEnumerable<IWordTreeViewItem> CreateItemsFromXml(XmlNode rootNode, IWordTreeViewItem rootNodeItem)
		{
			var result = new IWordTreeViewItem[rootNode.ChildNodes.Count];

			for(int i = 0; i < rootNode.ChildNodes.Count; i++)
			{
				XmlNode node = rootNode.ChildNodes[i];
				switch (node.Name)
				{
					case WordNode:
						result[i] = new WordViewModel(node.Attributes[TextAttribute].Value, rootNodeItem, _viewModel.DataService);
						break;
					case CategoryNode:
						var newNode = new WordCategoryViewModel(node.Attributes[TextAttribute].Value, rootNodeItem, _viewModel.DataService);
						var children = CreateItemsFromXml(node, newNode);

						newNode.Children = new System.Collections.ObjectModel.ObservableCollection<IWordTreeViewItem>(children);
						result[i] = newNode;
						break;
					default:
						throw new XmlException($"unknown node name {node.Name}");
				}
			}

			return result;
		}

		private static void AddAllItemsToTree(XmlDocument document, XmlElement parentNode, IEnumerable<IWordTreeViewItem> items)
		{
			foreach(var item in items)
			{
				XmlElement newNode;

				if (item.Children != null)
				{
					newNode = document.CreateElement(CategoryNode);
					AddAllItemsToTree(document, newNode, item.Children);
				}
				else
				{
					newNode = document.CreateElement(WordNode);
				}

				newNode.SetAttribute(TextAttribute, item.Text);
				parentNode.AppendChild(newNode);
			}
		}
	}
}
