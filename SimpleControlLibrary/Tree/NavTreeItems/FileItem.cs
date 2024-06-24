using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace SimpleControlLibrary.Tree
{

    public class FileItem : NavTreeItem
    {
        public FileItem(INavTreeItem parent) : base(parent)
        {
        }
        public FileItem() : base(null)
        {
        }
        public override BitmapSource GetMyIcon()
        {
            // to do, use a cache for .ext != "" or ".exe" or ".lnk"
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }

        public override ObservableCollection<INavTreeItem> GetMyChildren()
        {
            ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
            return childrenList;
        }
    }
}
