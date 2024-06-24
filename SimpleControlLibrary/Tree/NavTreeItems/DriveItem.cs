using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.IO;

namespace SimpleControlLibrary.Tree
{
    // DriveItem has Folders and Files as children
    public class DriveItem : NavTreeItem
    {
        public DriveItem() : base(null)
        {
        }

        public override BitmapSource GetMyIcon()
        {
            //string Param = "pack://application:,,,/" + "MyImages/diskdrive.png";
            //Uri uri1 = new Uri(Param, UriKind.RelativeOrAbsolute);
            //return myIcon = BitmapFrame.Create(uri1);
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }

        public override ObservableCollection<INavTreeItem> GetMyChildren()
        {
            ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
            INavTreeItem item1;

            DriveInfo drive = new DriveInfo(this.FullPathName);
            if (!drive.IsReady) return childrenList;

            DirectoryInfo di = new DirectoryInfo(((DriveInfo)drive).RootDirectory.Name);
            if (!di.Exists) return childrenList;

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                item1 = new FolderItem(this);
                item1.FullPathName = FullPathName + "\\" + dir.Name;
                item1.FriendlyName = dir.Name;
                item1.IncludeFileChildren = this.IncludeFileChildren;
                childrenList.Add(item1);
            }

            if (this.IncludeFileChildren)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    item1 = new FileItem(this);
                    item1.FullPathName = FullPathName + "\\" + file.Name;
                    item1.FriendlyName = file.Name;
                    childrenList.Add(item1);
                }
            }
            return childrenList;
        }
    }
}
