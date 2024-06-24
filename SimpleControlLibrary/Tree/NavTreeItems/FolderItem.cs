using System.Windows.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace SimpleControlLibrary.Tree
{
    public class FolderItem : NavTreeItem
    {
        public FolderItem(INavTreeItem parent) : base(parent)
        {
        }
        public FolderItem() : base(null)
        {
        }

        public override BitmapSource GetMyIcon()
        {
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }

        public override ObservableCollection<INavTreeItem> GetMyChildren()
        {
            ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
            INavTreeItem item1;

            try
            {
                DirectoryInfo di = new DirectoryInfo(this.FullPathName); // may be acces not allowed
                if (!di.Exists) return childrenList;
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    item1 = new FolderItem(this);
                    item1.FullPathName = FullPathName + "\\" + dir.Name;
                    item1.FriendlyName = dir.Name;
                    item1.IncludeFileChildren = this.IncludeFileChildren;
                    childrenList.Add(item1);
                }

                if (this.IncludeFileChildren) foreach (FileInfo file in di.GetFiles())
                    {
                        item1 = new FileItem(this);
                        item1.FullPathName = FullPathName + "\\" + file.Name;
                        item1.FriendlyName = file.Name;
                        childrenList.Add(item1);
                    }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return childrenList;
        }
    }
}
