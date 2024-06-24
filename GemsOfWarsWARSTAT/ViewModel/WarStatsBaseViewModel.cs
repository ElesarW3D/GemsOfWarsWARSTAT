using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsWARSTAT.DataContext;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using static GemsOfWarsMainTypes.GlobalConstants;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public abstract class WarStatsBaseViewModel : BaseVisualViewModel
    {
        private RelayCommand _addItem;
        private RelayCommand _deleteItem;
        private RelayCommand _updateItem;
        private RelayCommand _editingItem;
        private RelayCommand _undoItem;
        protected WarStatsModelViewModel _selection;
        private WarStatsModelViewModel _lastStateUser;
       

        protected bool _isEditing;

        private string _validationText;
        private bool _hasError = false;

        protected WarStatsBaseViewModel(WarDbContext context):base(context)
        {
            AddItem = new RelayCommand(OnAddItem, onAddItemCanExecute);
            UpdateItem = new RelayCommand(OnUpdateItem, OnUpdateCanExecute);
            EditingItem = new RelayCommand(OnEditItem, OnEditCanExecute);
            UndoItem = new RelayCommand(OnUndoItem, OnUndoCanExecute);
            DeleteItem = new RelayCommand(OnDeleteItem, OnDeleteCanExecute);
        }

        private bool OnDeleteCanExecute()
        {
            return Selection.IsNotNull() && !IsEditing;
        }

        private bool onAddItemCanExecute()
        {
            return !IsEditing;
        }

        private bool OnUndoCanExecute()
        {
            return _lastStateUser.IsNotNull() && IsEditing;
        }

        private void OnUndoItem()
        {
            Selection = _lastStateUser;
            IsEditing = false;
            HasError = false;
            ValidationText = string.Empty;
        }

        protected virtual bool OnUpdateCanExecute()
        {
            return Selection != null && IsEditing;
        }

        protected abstract void OnDeleteItem();
        protected abstract void OnUpdateItem();

        protected void DoOnUpdateItem()
        {
            IsEditing = false;
            LastStateUser = null;
            Context.SaveChanges();
        }

        protected void DoOnAddItem()
        {
            IsEditing = true;
            RaisePropertyChanged(nameof(Selection));
        }

        protected bool CheckValidation<T>(IEnumerable<T> items, string displayTable) 
            where T: IIdenty
             
        {
            HasError = false;
            ValidationText = "";

            if (Selection.IsNull())
            {
                HasError = true;
                ValidationText = NotSelect.Args(displayTable);
                return true;
            }
            var usersFind = items.Where(x => Selection.IsEquals(x));
            var isFind = usersFind.Count() > 1;
            if (!isFind || Selection.Id == 0)
            {
                foreach (var item in usersFind)
                {
                    var state = Context.Entry(item).State;
                    if (state != EntityState.Modified && item.Id != Selection.Id)
                    {
                        isFind = true;
                    }
                    if (isFind) break;
                }
                
            }
            if (isFind)
            {
                HasError = true;
                if (string.IsNullOrEmpty(ValidationText))
                {
                    ValidationText += "\n";
                }
                ValidationText += HasAlready.Args(displayTable);
                return true;
            }
            
            return false;

        }
        protected abstract void OnAddItem();
       
        private bool OnEditCanExecute()
        {
            return Selection != null && !IsEditing;
        }

        private void OnEditItem()
        {
            IsEditing = !IsEditing;
            _lastStateUser = (WarStatsModelViewModel)Selection.Clone();
        }

        public virtual WarStatsModelViewModel Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                _updateItem.RaiseCanExecuteChanged();
                _editingItem.RaiseCanExecuteChanged();
                _deleteItem.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(Selection));
            }
        }

        public virtual bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                   
                    RaisePropertyChanged(nameof(IsEditing));
                }
            }
        }

        public RelayCommand AddItem { get => _addItem; set => _addItem = value; }
        public RelayCommand UpdateItem { get => _updateItem; set => _updateItem = value; }
        public RelayCommand EditingItem { get => _editingItem; set => _editingItem = value; }
        public RelayCommand UndoItem { get => _undoItem; set => _undoItem = value; }
        protected WarStatsModelViewModel LastStateUser { get => _lastStateUser; set => _lastStateUser = value; }
        public RelayCommand DeleteItem { get => _deleteItem; set => _deleteItem = value; }

        public string ValidationText
        {
            get => _validationText;

            set
            {
                _validationText = value;
                RaisePropertyChanged(nameof(ValidationText));
            }
        }

        public bool HasError
        {
            get => _hasError;

            set
            {
                _hasError = value;
                RaisePropertyChanged(nameof(HasError));
            }
        }

        protected void SetSelectToLast<T>(ObservableCollection<T> mainCollection) where T : WarStatsModelViewModel
        {
            if (mainCollection.IsNotNull() && mainCollection.Any())
            {
                Selection = mainCollection.Last();
            }
        }
    }
}
