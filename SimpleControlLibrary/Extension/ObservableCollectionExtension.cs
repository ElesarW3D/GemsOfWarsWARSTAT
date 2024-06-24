using SimpleControlLibrary.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlLibrary.Extension
{
    public static class ObservableCollectionExtension
    {
        public static T GetFirstOf<T>(this ObservableCollection<INavTreeItem> collect) where T: INavTreeItem
        {
            foreach (var item in collect)
            {
                if (item is T coorrectItem)
                    return coorrectItem;
                if (item.Children.Any())
                    return GetFirstOf<T>(item.Children);
            }
            return default(T);
        }

        public static List<INavTreeItem> InDepthList(this ObservableCollection<INavTreeItem> collect) 
        {
            var list = new List<INavTreeItem>();
            foreach (var item in collect)
            {
                list.Add(item);
                if (item.Children.Any())
                {
                    InDepthList(item.Children, list);
                }
            }
            return list;
        }
        private static void InDepthList(this ObservableCollection<INavTreeItem> collect, List<INavTreeItem> result)
        {
            foreach (var item in collect)
            {
                result.Add(item);
                if (item.Children.Any())
                {
                    InDepthList(item.Children, result);
                }
            }
        }

    }
}
