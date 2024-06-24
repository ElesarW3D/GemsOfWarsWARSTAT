using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace SimpleControlLibrary.Tree
{
    // Model of tree items: NavTreeItem

    // This file: (INavTabItem) - (NavTabItem abstract Class as basis) 

    // See File NavTreeItems.cs for more specific NavTreeItem classes and how we use them. 
    // These specific classes define own method for icon, children and constructor
    
    // Interface INavTreeItem, just summary of class 
    // Normally better to define smaller interfaces and then compose INavTreeItem for a SOLID basis 
    public interface INavTreeItem : INotifyPropertyChanged
    {
        // For text in treeItem
        string FriendlyName { get; set; }

        // Image used in TreeItem
        BitmapSource MyIcon { get; set; }

        // Drive/Folder/File naming scheme to retrieve children
        string FullPathName { get; set; }

        ObservableCollection<INavTreeItem> Children { get; }

        bool IsExpanded { get; set; }

        // Design decisions: 
        // - do we use INotifyPropertyChanged. Maybe not quite aproporiate in model, but without MVVM framework practical shortcut
        // - do we introduce IsSelected, in most cases I would advice: Yes. I use now button+command to set Path EACH time item pressed
        bool IsSelected { get; set; }
        // void DeselectAll();
        bool IsChecked { get; set; }

        // Specific for this application, could be introduced later in more specific interface/classes
        bool IncludeFileChildren { get; set; }

        // For resetting the tree
        void DeleteChildren();

        INavTreeItem Parent { get; }
    }

    // Abstact classs next step to implementation
    public abstract class NavTreeItem : ViewModelBase, INavTreeItem
    {
        // for display in tree
        public string FriendlyName { get; set; }

        protected BitmapSource myIcon;
        public BitmapSource MyIcon 
        {
            get { return myIcon ?? (myIcon = GetMyIcon()); } 
            set { myIcon = value; } 
        }

        public string FullPathName { get; set; }

        protected ObservableCollection<INavTreeItem> children;
        public ObservableCollection<INavTreeItem> Children
        {
            get { return children ?? (children = GetMyChildren()); }
            set 
            { 
                children = value;
                RaisePropertyChanged();
            }
        }
        
        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set 
            { 
                _isExpanded = value;
                if (Parent != null)
                {
                    Parent.IsExpanded = _isExpanded;
                }
                RaisePropertyChanged(nameof(IsExpanded));
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        private bool _isChecked = false;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged(nameof(IsChecked));
            }
        }

        public bool IncludeFileChildren { get; set; }

        public INavTreeItem Parent { get; private set; }

        protected NavTreeItem(INavTreeItem parent)
        {
            Parent = parent;
        }

        public abstract BitmapSource GetMyIcon();
        public abstract ObservableCollection<INavTreeItem> GetMyChildren();

        public void DeleteChildren()
        {
            if (children != null)
            {
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    children[i].DeleteChildren();
                    children[i] = null;
                    children.RemoveAt(i);
                }

                children = null;
            }
        }
    }
}

