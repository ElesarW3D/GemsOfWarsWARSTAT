using System.Windows.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace SimpleControlLibrary.Tree
{
    // Definition of several NavTreeItem classes (constructor, GetMyIcion and GetMyChildren) from abstact class NavTreeItem 
    // Note that this file can be split/refactored in smaller parts

    // RootItems
    // - Special items are "RootItems" such as DriveRootItem with as children DriveItems
    //   other RootItems might be DriveNoChildRootItem, FavoritesRootItem, SpecialFolderRootItem, 
    //   (to do) LibraryRootItem, NetworkRootItem, HistoryRootItem.
    // - We use RootItem(s) as a RootNode for trees, their Children (for example DriveItems) are copied to RootChildren VM
    // - Binding in View: TreeView.ItemsSource="{Binding Path=NavTreeVm.RootChildren}"

    // DriveRootItem has DriveItems as children 
    public class DriveRootItem : NavTreeItem
    {
        public DriveRootItem():base(null)
        {
            //Constructor sets some properties
            FriendlyName = "DriveRoot";
            IsExpanded = true;
            FullPathName = "$xxDriveRoot$";
        }

        public override BitmapSource GetMyIcon()
        {
            // Note: introduce more "speaking" icons for RootItems
            string Param = "pack://application:,,,/" + "MyImages/bullet_blue.png";
            Uri uri1 = new Uri(Param, UriKind.RelativeOrAbsolute);
            return myIcon = BitmapFrame.Create(uri1);
        }

        public override ObservableCollection<INavTreeItem> GetMyChildren()
        {
            ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
            INavTreeItem item1;
            string fn = "";

            //string[] allDrives = System.Environment.GetLogicalDrives();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
                if (drive.IsReady)
                {
                    item1 = new DriveItem();

                    // Some processing for the FriendlyName
                    fn = drive.Name.Replace(@"\", "");
                    item1.FullPathName = fn;
                    if (drive.VolumeLabel == string.Empty)
                    {
                        fn = drive.DriveType.ToString() + " (" + fn + ")";
                    }
                    else if (drive.DriveType == DriveType.CDRom)
                    {
                        fn = drive.DriveType.ToString() + " " + drive.VolumeLabel + " (" + fn + ")";
                    }
                    else
                    {
                        fn = drive.VolumeLabel + " (" + fn + ")";
                    }

                    item1.FriendlyName = fn;
                    item1.IncludeFileChildren = this.IncludeFileChildren;
                    childrenList.Add(item1);
                }

            return childrenList;
        }
    }
}
