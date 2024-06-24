using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using SimpleControlLibrary.Extension;
using GalaSoft.MvvmLight.CommandWpf;
using System.Diagnostics;

namespace SimpleControlLibrary.Tree
{
    public class NavTreeVm : ViewModelBase
    {
        private string _treeName = "";
        private NavTreeItem _currentItem;
        private ObservableCollection<INavTreeItem> _rootChildren = new ObservableCollection<INavTreeItem> { };
        
        public NavTreeVm(string fullPathName)
        {
            NavTreeItem treeRootItem = NavTreeRootItemUtils.ReturnRootItem(0, true);
            treeRootItem.FullPathName = fullPathName;
            TreeName = treeRootItem.FriendlyName;

            foreach (var item in treeRootItem.Children)
            {
                RootChildren.Add(item);
            }
            treeRootItem.IsExpanded = true;
            CurrentItem = RootChildren.GetFirstOf<FileItem>();
            SetCurrentItem(CurrentItem);
            InDepthList = RootChildren.InDepthList();
            InDepthDictionary = InDepthList.ToDictionary(x => x.FullPathName);
            CurrentIndexDepth = InDepthList.IndexOf(CurrentItem);
            Debug.Assert(CurrentIndexDepth >= 0);

            
        }

        public bool CanNext()
        {
            var index = CurrentIndexDepth;
            index++;
            while (index < InDepthList.Count)
            {
                if (InDepthList[index] is FileItem)
                {
                    return true;
                }
                index++;
            }
            return false;
        }

        public void OnNext()
        {
            var index = CurrentIndexDepth;
            index++;
            while (index < InDepthList.Count)
            {
                if (InDepthList[index] is FileItem fileItem)
                {
                    CurrentIndexDepth = index;
                    SetCurrentItem(fileItem);
                    return;
                }
                index++;
            }
            Debug.Assert(false);
        }

        private void SetCurrentItem(NavTreeItem navTreeItem)
        {
            if (CurrentItem != null)
            {
                CurrentItem.IsExpanded = false;
                CurrentItem.IsSelected = false;
            }
           
            CurrentItem = navTreeItem;
            navTreeItem.IsExpanded = true;
            navTreeItem.IsSelected = true;
            RaisePropertyChanged(nameof(CurrentItem.FullPathName));
        }

        public void TrySelectItem(string item)
        {
            int index = findIndexByName(item);
            if (index < 0)
            {
                return;
            }
            while (index < InDepthList.Count)
            {
                if (InDepthList[index] is FileItem fileItem)
                {
                    CurrentIndexDepth = index;
                    SetCurrentItem(fileItem);
                    return;
                }
                index++;
            }
            Debug.Assert(false);
        }

        private int findIndexByName(string item)
        {
            return InDepthList.Select((it, ind) => new { it, ind }).FirstOrDefault(it => it.it.FullPathName == item)?.ind ?? -1;
        }

        public bool CanPrevious()
        {
            var index = CurrentIndexDepth;
            index--;
            while (index >= 0)
            {
                if (InDepthList[index] is FileItem)
                {
                    return true;
                }
                index--;
            }
            return false;
        }

        public void OnPrevious()
        {
            var index = CurrentIndexDepth;
            index--;
            while (index >= 0)
            {
                if (InDepthList[index] is FileItem fileItem )
                {
                    CurrentIndexDepth = index;
                    SetCurrentItem(fileItem);
                    return;
                }
                index--;
            }
            Debug.Assert(false);
        }

        public string TreeName
        {
            get { return _treeName; }
            set 
            { 
                _treeName = value;
                RaisePropertyChanged();
            }
        }

        public NavTreeItem CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                RaisePropertyChanged(nameof(CurrentItem));
            }
        }

        public ObservableCollection<INavTreeItem> RootChildren
        {
            get { return _rootChildren; }
            set 
            { 
                _rootChildren = value;
                RaisePropertyChanged(nameof(RootChildren));
            }
        }

        public List<INavTreeItem> InDepthList { get; private set; }
        public Dictionary<string, INavTreeItem> InDepthDictionary { get; private set; }
        public int CurrentIndexDepth { get; private set; }

    }
}



