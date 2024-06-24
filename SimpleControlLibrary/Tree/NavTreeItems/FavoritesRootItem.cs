using System.Windows.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace SimpleControlLibrary.Tree
{
    // FavoritesRootItem has Windows7 "File Explorer" Favorites as children, will not work on non windows 7 systems
    // Does not work quite properly, cannot find documentation. Did some hacking but order of items unkown 
    // If you don't have windows7 remove/rename this class/constructor ...Root_Item 
    // Or choose your own folder see (**) and fill it with drive/folder shortcuts
    //public class FavoritesRootItem : NavTreeItem
    //{
    //    public FavoritesRootItem()
    //    {
    //        FriendlyName = "Favorites"; // tmp hack: fixed Name
    //        FullPathName = "$xxFavoritesRoot$";
    //        IsExpanded = true;
    //    }

    //    public override BitmapSource GetMyIcon()
    //    {
    //        // to do: nice icon for this ItemRoots
    //        string Param = "pack://application:,,,/" + "MyImages/bullet_blue.png";
    //        Uri uri1 = new Uri(Param, UriKind.RelativeOrAbsolute);
    //        return myIcon = BitmapFrame.Create(uri1);
    //    }

    //    public override ObservableCollection<INavTreeItem> GetMyChildren()
    //    {
    //        ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
    //        INavTreeItem item1;
    //        string fn = "";

    //        // This does not work yet properly: know the folder, note also desktop.ini present
    //        // 1) Localisation of name and path 
    //        // 2) How is the order specified, cannot find documentation

    //        // If not Windows7, no children. I cannot test this now.
    //        if (!Utils.TestCurrentOs.IsWindows7()) return childrenList;

    //        // tmp hack, fixed filename. 
    //        // (**) Non Windows 7: Specify fn= your own favorites folder and put some Drive/Folder shortcuts in this folder      
    //        Environment.SpecialFolder s = Environment.SpecialFolder.UserProfile;
    //        fn = Environment.GetFolderPath(s);
    //        fn = fn + "\\Links";  

    //        try
    //        {
    //            // For favorites we always return files!!
    //            DirectoryInfo di = new DirectoryInfo(fn);
    //            if (!di.Exists) return childrenList;

    //            string fileResolvedShortCut;

    //            foreach (FileInfo file in di.GetFiles())
    //            {
    //                if (file.Name.ToUpper().EndsWith(".LNK"))
    //                {
    //                    // tmp hack: resolve link to display icons instead of link-icons
    //                    fileResolvedShortCut = FolderPlaneUtils.ResolveShortCut(file.FullName);
    //                    if (fileResolvedShortCut != "")
    //                    {
    //                        FileInfo fileNs = new FileInfo(fileResolvedShortCut);

    //                        // to do localisation, names??
    //                        item1 = new FileItem();
    //                        item1.FullPathName = fileNs.FullName;
    //                        item1.FriendlyName = (fileNs.Name != "") ? fileNs.Name : fileNs.ToString();

    //                        childrenList.Add(item1);
    //                    }
    //                }
    //            }
    //        }
    //        catch
    //        {

    //        }
    //        return childrenList;
    //    }
    //}
}
