using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsWARSTAT.Services;
using GemsOfWarsWARSTAT.TextAnalysis;
using SimpleControlLibrary.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static GemsOfWarsMainTypes.GlobalConstants;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public partial class WarDayImportViewModel : WarsDayAddViewModel
    {
        private string _fileName;
        private NavTreeVm _singleTree;
        protected ObservableCollection<FileLocation> _fileLocations;
        protected ObservableCollection<MultipleFileLocations> _multipleFileLocations;
        private ObservableCollection<Guild> _guilds;
        public ObservableCollection<FileLocation> FileLocations
        {
            get { return _fileLocations; }
            set
            {
                _fileLocations = value;
                RaisePropertyChanged(nameof(FileLocations));
            }
        }
        public ObservableCollection<MultipleFileLocations> MultipleFileLocations
        {
            get { return _multipleFileLocations; }
            set
            {
                _multipleFileLocations = value;
                RaisePropertyChanged(nameof(MultipleFileLocations));
            }
        }
        public ObservableCollection<Guild> Guilds
        {
            get { return _guilds; }
            set
            {
                _guilds = value;
                RaisePropertyChanged(nameof(Guilds));
            }
        }
        public NavTreeVm SingleTree
        {
            get => _singleTree;
            set
            {
                _singleTree = value;
                RaisePropertyChanged(nameof(SingleTree));
            }
        }

        public WarDayImportViewModel(WarDbContext context) : base(context)
        {
            TryReadFromImage = new RelayCommand(OnTryReadFromImage, CanTrayToRead);
            PreviousCommand = new RelayCommand(OnPrevious, CanPrevious);
            NextCommand = new RelayCommand(OnNext, CanNext);
            SelectedPathFromTreeCommand = new RelayCommand<string>(OnSelect);
        }

        private bool CanTrayToRead()
        {
            if (SingleTree == null)
            {
                return false;
            }
            return !IsEditing && !SingleTree.CurrentItem.IsChecked;
        }

        private void OnSelect(string itemName)
        {
            if (IsEditing)
            {
                return;
            }
            SingleTree?.TrySelectItem(itemName);
            ActionWhenSelectImageChange();
        }

        private bool CanNext()
        {
            return !IsEditing && (SingleTree?.CanNext() ?? false);
        }

        private void OnNext()
        {
            SingleTree?.OnNext();
            ActionWhenSelectImageChange();
        }

        private void OnPrevious()
        {
            SingleTree?.OnPrevious();
            ActionWhenSelectImageChange();
        }

        private bool CanPrevious()
        {
           return !IsEditing && (SingleTree?.CanPrevious() ?? false);
        }

        public override VisiblityControls ViewModelType => VisiblityControls.WarDayImport;

        public string FileName 
        { 
            get => _fileName;
            set
            {
                _fileName = value;
                if (string.IsNullOrEmpty(value))
                {
                    return ;
                }
                SingleTree = new NavTreeVm(value);
                SelectCheckedToReadItem(SingleTree);
                RaisePropertyChanged(nameof(FileName));
                RaisePropertyChanged(nameof(SingleTree));
                ActionWhenSelectImageChange();
            }
        }

        private void SelectCheckedToReadItem(NavTreeVm singleTree)
        {
            var items = singleTree.InDepthDictionary;
            foreach (var item in items)
            {
                var value = item.Value;
                if (Context.FileLocations.FirstOrDefault(x=>x.Path == value.FullPathName) != null)
                {
                    value.IsChecked = true;
                }
            }
        }

        private void ActionWhenSelectImageChange()
        {
            RaisePropertyChanged(nameof(SelectImageFile));
            Selection = TryReadSelectionWarDay();
        }

        public string SelectImageFile => SingleTree?.CurrentItem.FullPathName;

        public override void Load()
        {
            //_initSelection = false;
            base.Load();
            Context.FileLocations.Load();
            FileLocations = Context.FileLocations.Local.ToObservableCollection();

            Context.MultipleFileLocations.Load();
            MultipleFileLocations = Context.MultipleFileLocations.Local.ToObservableCollection();

            Context.Guilds.Load();
            Guilds = Context.Guilds.Local.ToObservableCollection();
            Selection = TryReadSelectionWarDay();
        }

        private WarDay TryReadSelectionWarDay()
        {
            if (SelectImageFile != null)
            {
                var findDay = Context.MultipleFileLocations.FirstOrDefault(loc => loc.FileLocation.Path == SelectImageFile);
                if (findDay != null)
                {
                    return findDay.WarDay;
                }
            }
            return new WarDay();
        }

        public ICommand TryReadFromImage { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand SelectedPathFromTreeCommand { get; set; }
        protected override void OnUpdateItem()
        {
            var warsDays = Context.WarDays.Local.ToArray();
            if (CheckValidation(warsDays, DisplayWarDay))
            {
                return;
            }

            var addDefence = SelectionWarDay.Defence;
            var defence = Defences.FirstOrDefault(it => it.IsEqualsWithoutCode(addDefence));
            if (defence.IsNull())
            {
                var message = Messages.AddDefence.Args(addDefence.DisplayName);
                var result = MessageBox.Show(message, "Добавить защиту", MessageBoxButton.OKCancel);
                if (result != MessageBoxResult.OK)
                {
                    return;
                }
                Context.Defences.Add(addDefence);
                Context.SaveChanges();
                SelectionWarDay.Defence = addDefence;
            }
            else
            {
                SelectionWarDay.Defence = defence;
            }

            var adduser = SelectionWarDay.User;
            var user = Context.Users.FirstOrDefault(it => it.Name == adduser.Name);
            if (user.IsNull())
            {
                Context.Users.Add(adduser);
            }
            else
            {
                SelectionWarDay.User = user;
            }

            var warDay = Context.WarDays.FirstOrDefault(it => it.Id == SelectionWarDay.Id);
            if (warDay == null)
            {
                warDay = Context.WarDays.Add(SelectionWarDay);
            }
            string directoryNick;
            directoryNick = Path.GetDirectoryName(SelectImageFile);
            var userFolderInput = new UserFolderLocation()
            {
                Path = directoryNick,
                User = warDay.User
            };

            var userFolder = Context.FileLocations.OfType<UserFolderLocation>()
                .FirstOrDefault(uf => uf.Path == userFolderInput.Path && uf.User.Id == userFolderInput.User.Id);
            if (userFolder == null)
            {
                userFolder = Context.FileLocations.Add(userFolderInput) as UserFolderLocation;
            }

            var guild = FindGuid(directoryNick, out var directoryGuild);
            var guildFolderInput = new GuildFolderLocation()
            {
                Path = directoryGuild,
                Guild = guild
            };

            var guildFolder1 = Context.FileLocations.OfType<GuildFolderLocation>().ToArray();
            var guildFolder = guildFolder1
                .FirstOrDefault(uf => uf.Path == directoryGuild && uf.Guild.Id == guildFolderInput.Guild.Id);
            if (guildFolder == null)
            {
                guildFolder = Context.FileLocations.Add(guildFolderInput) as GuildFolderLocation;
            }

            var warPath = new FileLocation()
            {
                Path = SelectImageFile
            };
            warPath = Context.FileLocations.Add(warPath);

            var multiFileLocationWar = new MultipleFileLocations()
            {
                FileLocation = warPath,
                WarDay = warDay,
            };
            var multiFileLocationGuild = new MultipleFileLocations()
            {
                FileLocation = guildFolder,
                WarDay = warDay,
            };

            var multiFileLocationUser = new MultipleFileLocations()
            {
                FileLocation = userFolder,
                WarDay = warDay,
            };

            var itemsToChecked = new[] { multiFileLocationWar, multiFileLocationGuild, multiFileLocationUser };
            Context.MultipleFileLocations
                .AddRange(itemsToChecked);
            SetCheckedToItem(itemsToChecked);

            CheckDefence();
            DoOnUpdateItem();
        }

        private void SetCheckedToItem(MultipleFileLocations[] itemsToChecked)
        {
            var items = SingleTree?.InDepthDictionary;
            if (items != null)
            {
                foreach (var fileLocation in itemsToChecked)
                {
                    var path = fileLocation.FileLocation.Path;
                    Debug.Assert(items.ContainsKey(path));
                    if (items.ContainsKey(path))
                    {
                        items[path].IsChecked = true;
                    }
                }
            }
        }
    }
}
