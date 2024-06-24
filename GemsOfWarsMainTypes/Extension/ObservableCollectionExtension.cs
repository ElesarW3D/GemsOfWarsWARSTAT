using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Extension
{
    public static class ObservableCollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (T item in items)
            {
                collection.Add(item);
            }
            return collection;
        }

        public static void AddRange<T>(this ObservableCollection<T> items, IEnumerable<T> addItems)
        {
            foreach (T item in addItems)
            {
                items.Add(item);
            }
        }
    }
}
