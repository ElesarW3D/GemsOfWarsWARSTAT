using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace SimpleControlLibrary.Tree
{
    // DriveItem has no children, is never expanded. Somewaht usefull to start from unexpanded drives//tempory addition of RootItems
    public class DriveNoChildItem : NavTreeItem
    {
        public DriveNoChildItem() : base(null)
        {
        }

        public override BitmapSource GetMyIcon()
        {
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }

        public override ObservableCollection<INavTreeItem> GetMyChildren()
        {
            ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
            return childrenList;
        }
    }
}
