using System.Windows.Media.Imaging;
using System;
using System.Collections.ObjectModel;

namespace SimpleControlLibrary.Tree
{
    // SpecialFolderRootItem has SpecialFolders as Children. Not so usefull, tempory addition to have some RootItems
    // For compatability windows XP use in this example SpecialFolders instead of KnownFolders 
    public class SpecialFolderRootItem : NavTreeItem
    {
        public SpecialFolderRootItem(INavTreeItem navTreeItem):base(navTreeItem)
        {
            FriendlyName = "SpecialFolderRoot";
            FullPathName = "$xxSpecialFolderRoot$";
        }

        public override BitmapSource GetMyIcon()
        {
            string Param = "pack://application:,,,/" + "MyImages/bullet_blue.png";
            Uri uri1 = new Uri(Param, UriKind.RelativeOrAbsolute);
            return myIcon = BitmapFrame.Create(uri1);
        }

        public override ObservableCollection<INavTreeItem> GetMyChildren()
        {
            ObservableCollection<INavTreeItem> childrenList = new ObservableCollection<INavTreeItem>() { };
            INavTreeItem item1;
            string fn = "";

            // If not Windows7, no children? 
            // I use specialFolders instead of KnownFolders for comaptability of older OS, to do: test if this works  
            // if (!Utils.TestCurrentOs.IsWindows7()) return childrenList;

            // We show all items, incl. hidden

            var allSpecialFoldersV = Enum.GetValues(typeof(System.Environment.SpecialFolder));
            foreach (Environment.SpecialFolder s in allSpecialFoldersV)
            {
                fn = Environment.GetFolderPath(s);
                if (fn != string.Empty)
                {
                    item1 = new FolderItem(this);
                    item1.FullPathName = fn;
                    item1.FriendlyName = s.ToString();
                    item1.IncludeFileChildren = true;
                    item1.IncludeFileChildren = this.IncludeFileChildren;
                    childrenList.Add(item1);
                }
            }

            return childrenList;
        }
    }
}
