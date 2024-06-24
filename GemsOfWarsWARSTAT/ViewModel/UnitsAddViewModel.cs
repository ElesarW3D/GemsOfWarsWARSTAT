using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Model;
using System;
using System.Linq;
using System.Windows.Input;
using GemsOfWarsMainTypes;
using GemsOfWarsWARSTAT.DataContext;
using System.ComponentModel;
using GemsOfWarsMainTypes.SubType;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GemsOfWarsMainTypes.Extension;
using static GemsOfWarsMainTypes.GlobalConstants;
using GemsOfWarsWARSTAT.Services;
using System.Diagnostics;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class UnitsAddViewModel : WarStatsBaseViewModel
    {
        private ObservableCollection<Unit> _unitList;
        private ObservableCollection<HeroClass> _heroClasses;
        private HeroClass _selectionHero;
        

        private ICommand _addWeapon;
        private ICommand _cloneItem;
        public ObservableCollection<Unit> Units
        {
            get { return _unitList; }
            set 
            { 
                _unitList = value; 
                RaisePropertyChanged(nameof(Units));
            }
        }

        public ObservableCollection<HeroClass> HeroClasses
        {
            get { return _heroClasses; }
            set
            {
                _heroClasses = value;
                RaisePropertyChanged(nameof(HeroClasses));
            }
        }

        public UnitsAddViewModel(WarDbContext context):base(context)
        {
            AddWeapon = new RelayCommand(OnAddWeapon, OnAddWeaponCanExecute);
            CloneItem = new RelayCommand(OnCloneItem,OnCloneItemCanExecute);

        }

        private void OnCloneItem()
        {
            Unit newItem = null;
            if (IsWeapon)
            {
                newItem = new HeroWeapon()
                {
                    Name = HeroWeapon.Name,
                    GameId = HeroWeapon.GameId,
                    ColorUnits = HeroWeapon.ColorUnits,
                    HeroClassId = HeroWeapon.HeroClassId,
                };

            }
            else
            {
                newItem = new Unit()
                {
                    GameId = SelectionUnit.GameId,
                    Name = SelectionUnit.Name,
                    ColorUnits = SelectionUnit.ColorUnits,
                };

            }
            Debug.Assert(newItem != null);
            if (newItem != null)
            {
                Selection = newItem;
                IsEditing = true;
            }
        }

        private bool OnCloneItemCanExecute()
        {
            return !IsEditing;
        }

        private bool OnAddWeaponCanExecute()
        {
            return !IsEditing;
        }

        public override void Load()
        {
            Context.Units.Load();
            Units = Context.Units.Local.OrderBy(x => x.DisplayName).ToObservableCollection();
            
            Context.HeroClasses.Load();
            HeroClasses = Context.HeroClasses.Local.ToObservableCollection();
            
            SetSelectToLast(Units);
        }

        protected override bool OnUpdateCanExecute()
        {
            var hasSelection = Selection != null && IsWeapon ? SelectionHero != null : true;
            return base.OnUpdateCanExecute() && hasSelection;
        }

        private void OnAddWeapon()
        {
            var newItem = new HeroWeapon()
            {
                Name = GlobalConstants.NonameUnit,
                ColorUnits = ColorUnits.Red,
                HeroClassId = 1,
            };

            Selection = newItem;
            Units.Add(newItem);
            IsEditing = true;
        }

        protected override void OnAddItem()
        {
            var newItem = new Unit()
            {
                Name = GlobalConstants.NonameUnit,
                ColorUnits = ColorUnits.Red
            };

            Selection = newItem;
            Units.Add(newItem);
            DoOnAddItem();
        }

        protected override void OnDeleteItem()
        {
            var unit = Context.Units.FirstOrDefault(it => it.Id == SelectionUnit.Id);

            Units.Remove(SelectionUnit);
            Selection = null;
            if (unit != null)
            {
                Context.Units.Remove(unit);
            }

            DoOnUpdateItem();

            SetSelectToLast(Units);
        }

        protected override void OnUpdateItem()
        {
            var units = Context.Units.Local.ToArray();
            if (IsWeapon)
            {
                var weapons = units.OfType<HeroWeapon>().ToArray();
                if (CheckValidation(weapons, DisplayUnit))
                {
                    return;
                }
            }
            else
            {
                if (CheckValidation(units, DisplayUnit))
                {
                    return;
                }
            }
           

            var unit = Context.Units.FirstOrDefault(it => it.Id == SelectionUnit.Id);
            if (unit != null)
            {
                unit.ReadFromItem(SelectionUnit);
            }
            else
            {
                Context.Units.Add(SelectionUnit);
            }

            DoOnUpdateItem();
        }

        public override WarStatsModelViewModel Selection
        {
            get { return _selection; }
            set 
            {
                base.Selection = value;
                if (IsWeapon)
                {
                    SelectionHero = HeroWeapon?.HeroClass;
                }
                RaisePropertyChanged(nameof(SelectionUnit.DisplayName));
                RaisePropertyChanged(nameof(Selection));
                RaisePropertyChanged(nameof(NameColor));
                RaisePropertyChanged(nameof(IsWeapon));
            }
        }

        public HeroClass SelectionHero
        {
            get { return _selectionHero; }
            set
            {
                _selectionHero = value;
                HeroWeapon.HeroClass = value;
                RaisePropertyChanged(nameof(Units));
                RaisePropertyChanged(nameof(Selection));
                RaisePropertyChanged(nameof(SelectionHero));
            }
        }
      
        public ICommand AddWeapon
        {
            get { return _addWeapon; }
            set { _addWeapon = value; }
        }
        public ICommand CloneItem
        {
            get { return _cloneItem; }
            set { _cloneItem = value; }
        }

        public string NameColor => SelectionUnit?.ColorUnits.ToString();

        public bool IsWeapon => Selection is HeroWeapon;
        public HeroWeapon HeroWeapon => Selection as HeroWeapon;

        public Unit SelectionUnit => Selection as Unit;

        public override VisiblityControls ViewModelType => VisiblityControls.UnitsAdd;
    }
}
