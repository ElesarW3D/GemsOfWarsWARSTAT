using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GemsOfWarsMainTypes;
using GemsOfWarsWARSTAT.DataContext;
using System.ComponentModel;
using System.Data.Entity;
using SimpleControlLibrary.SelectControl.DefenceControl;
using GemsOfWarsMainTypes.Extension;
using static GemsOfWarsMainTypes.GlobalConstants;
using System;
using System.Windows;
using GemsOfWarsWARSTAT.Services;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class DefencesAddViewModel : WarStatsBaseViewModel
    {
        private ObservableCollection<Defence> _defenceList;
        private ObservableCollection<Unit> _unitsList;
        private DefencesAgregatorViewModel _agregatorViewModel;
        private bool isInitDefencesAgregatorViewModels = false;
        private RelayCommand _copyToBuffer;
        public ObservableCollection<Defence> Defences
        {
            get { return _defenceList; }
            set 
            { 
                _defenceList = value; 
                RaisePropertyChanged(nameof(Defences));
            }
        }

        public ObservableCollection<Unit> Units
        {
            get { return _unitsList; }
            set
            {
                _unitsList = value;
                RaisePropertyChanged(nameof(Units));
            }
        }

        public DefencesAddViewModel(WarDbContext context):base(context)
        {
            AgregatorViewModel = new DefencesAgregatorViewModel();
            CopyToBuffer = new RelayCommand(OnCopy, CanCopy);
        }

        private bool CanCopy()
        {
            return GetEmptyCodeDefence().Any();
        }

        private IQueryable<Defence> GetEmptyCodeDefence()
        {
            return Context.Defences.Where(x => string.IsNullOrEmpty(x.Code));
        }

        private void OnCopy()
        {
            var buffer = "";
            foreach (var item in GetEmptyCodeDefence().OrderByUnits())
            {
                buffer += item.DisplayName.ToString() + ". \t" + Environment.NewLine;
               
            }
            Clipboard.SetText(buffer);
        }

        public override void Load()
        {
            Context.Defences.Load();
            Context.Defences
                .Include(db => db.Units1)
                .Include(db => db.Units2)
                .Include(db => db.Units3)
                .Include(db => db.Units4)
                .Load();

            Defences = Context.Defences.Local.ToArray()
                .OrderByUnits()
                .ToObservableCollection();
            
            Context.Units.Load();
            _unitsList = Context.Units.Local.ToArray().OrderBy(x => x.DisplayName).ToObservableCollection();

            SetSelectToLast(Defences);


            AgregatorViewModel.SetNewIDefences(SelectionDefence);
            if (!isInitDefencesAgregatorViewModels)
            {
                AgregatorViewModel.InitReactionControl();
                isInitDefencesAgregatorViewModels = true;
            }
            AgregatorViewModel.InitUnitsProperty(_unitsList);
            RaisePropertyChanged(nameof(AgregatorViewModel));

            Context.HeroClasses.Load();
        }

        private Defence CreateNewItem()
        {
            Context.Units.Load();
            var weapon = Context.Units.FirstOrDefault( it => it is HeroWeapon);
            var teamWithWeapon = weapon != null;
            var countUnit = teamWithWeapon ? GlobalConstants.CountInTeam : GlobalConstants.CountInTeam -1 ;
            var units = Context.Units;
            var weapons = units.Where(it => (it is HeroWeapon));
            var onlyUnits = units.Where(it => !(it is HeroWeapon));
            List<Unit> defenceList = null;
            if (teamWithWeapon)
            {
                defenceList = weapons.Take(1).Concat(onlyUnits.Take(3)).ToList();
            }
            else
            {
                defenceList = onlyUnits.Take(4).ToList();
            }

            var defence = new Defence()
            {
                Units1 = defenceList[0],
                Units2 = defenceList[1],
                Units3 = defenceList[2],
                Units4 = defenceList[3],
            };
            return defence;

        }

        protected override void OnDeleteItem()
        {
            var defence = Context.Defences.FirstOrDefault(it => it.Id == SelectionDefence.Id);

            Defences.Remove(SelectionDefence);
            Selection = null;
            if (defence != null)
            {
                Context.Defences.Remove(defence);
            }

            DoOnUpdateItem();

            SetSelectToLast(Defences);
        }

        protected override void OnUpdateItem()
        {
            var defences = Context.Defences.Local.ToArray();
            if (CheckValidation(defences, DisplayDefence))
            {
                return;
            }

            var defence = Context.Defences.FirstOrDefault(it => it.Id == SelectionDefence.Id);
            if (defence != null)
            {
                defence.ReadFromItem(SelectionDefence);
            }
            else
            {
                Context.Defences.Add(SelectionDefence);
            }

            DoOnUpdateItem();
        }

        protected override void OnAddItem()
        {
            var newItem = new Defence()
            {
                Units1 = SelectionDefence.Units1,
                Units2 = SelectionDefence.Units2,
                Units3 = SelectionDefence.Units3,
                Units4 = SelectionDefence.Units4,
            };

            Defences.Add(newItem);
            Selection = newItem;
            DoOnAddItem();
        }


        public override WarStatsModelViewModel Selection
        {
            get { return _selection; }
            set
            {
                base.Selection = value;
                if (AgregatorViewModel.IsNotNull() && SelectionDefence.IsNotNull())
                {
                    AgregatorViewModel.SetNewIDefences(SelectionDefence);
                    RaisePropertyChanged(nameof(SelectionDefence.Code));
                    RaisePropertyChanged(nameof(AgregatorViewModel));
                }
              
                RaisePropertyChanged(nameof(Selection));
            }
        }

        public Defence SelectionDefence => Selection as Defence;

        public override bool IsEditing 
        { 
            get => base.IsEditing;
            set
            {
                if (AgregatorViewModel.IsNotNull())
                {
                    AgregatorViewModel.IsEditing = value;
                }

                base.IsEditing = value;
            }
        }

        public DefencesAgregatorViewModel AgregatorViewModel 
        { 
            get => _agregatorViewModel;
            set 
            { 
                _agregatorViewModel = value; 
                RaisePropertyChanged(nameof(AgregatorViewModel));
            }
        }

        public RelayCommand CopyToBuffer { get => _copyToBuffer; set => _copyToBuffer = value; }

        public override VisiblityControls ViewModelType => VisiblityControls.DefenceAdd;
    }
}
