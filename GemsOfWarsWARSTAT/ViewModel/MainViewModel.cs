using GalaSoft.MvvmLight;
using GemsOfWarsWARSTAT.DataContext;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsWARSTAT.Services;
using System;
using GemsOfWarsWARSTAT.ViewModel.Statistic;
using GemsOfWarsMainTypes.Extension;
using SimpleControlsLibrary.Dialogs.DialogFacade;
using SimpleControlsLibrary.Dialogs.DialogService;
using GemsOfWarsWARSTAT.TextAnalysis;
using System.IO;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelLocator _viewModelLocator;
        private VisibilityMainControls _visibilityMainControls;
        private DialogFacade _dialogFacade;
       
        private ICommand _openViewModel;

        private DefencesAddViewModel _defences;
        private MainUserInGuildAddViewModel _users;
        private UnitsAddViewModel _units;
        private WarsAddViewModel _wars;
        private WarsDayAddViewModel _warsDay;
        private StatisticMainViewModel _statistic;
        private RealStatAddViewModel _realStat;
        private ChartStatisticViewModel _chartStatistic;
        private WarDayImportViewModel _warDayImport;

        private Visibility _visibilityMainGrid = Visibility.Hidden;
        public MainViewModel()
        {
            _viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            _visibilityMainControls = _viewModelLocator.VisibilityMainControls;
            _dialogFacade = _viewModelLocator.DialogFacade;
            
            _defences = new DefencesAddViewModel(WarDbContext);
            _users = new MainUserInGuildAddViewModel(WarDbContext);
            _units = new UnitsAddViewModel(WarDbContext);
            _wars = new WarsAddViewModel(WarDbContext);
            _warsDay = new WarsDayAddViewModel(WarDbContext);
            _statistic = new StatisticMainViewModel(WarDbContext);
            _realStat = new RealStatAddViewModel(WarDbContext);
            _chartStatistic = new ChartStatisticViewModel(WarDbContext);
            _warDayImport = new WarDayImportViewModel(WarDbContext);

            OpenViewModel = new RelayCommand<BaseVisualViewModel>(OnOpenViewModel);
            SaveAs = new RelayCommand<object>(OnSaveAs);
            Import = new RelayCommand<object>(OnImport);
        }

        private void OnImport(object window)
        {
            var result = _dialogFacade.ShowDialogImport("Выберите файл", WarDayImport.FileName, window as Window);

            if (result == CustomDialogResult.Yes && Directory.Exists(_dialogFacade.FileName))
            {
                //WarDayReader warDayReader = new WarDayReader(WarDbContext);
                //var outputString = warDayReader.ReadText(_dialogFacade.FileName);
                WarDayImport.FileName = _dialogFacade.FileName;
                OnOpenViewModel(WarDayImport);

            }
            else if (result == CustomDialogResult.Yes)
            {
                OnOpenViewModel(WarDayImport);
            }
            else
            {
                WarDayImport.FileName = "";
            }
        }

        private void OnSaveAs(object window)
        {
            var result = _dialogFacade.ShowDialogSave("Сохранить файл как", window as Window);
            if (result == CustomDialogResult.Yes)
            {
                var newContext = new WarDbContext(_dialogFacade.FileName);
                WarDbContext.CopyTo(newContext);

                newContext.SaveChanges();
                newContext.Dispose();
            }
        }

        private void OnOpenViewModel(BaseVisualViewModel baseVisualView)
        {

            if (baseVisualView.IsNull())
            {
                return;
            }

            VisibilityMainGrid = Visibility.Hidden;
            _visibilityMainControls.SetOneVisibility(baseVisualView.ViewModelType);
            baseVisualView.Load();
            VisibilityMainGrid = Visibility.Visible;
           
        }

        #region ViewModels
        public WarDbContext WarDbContext => _viewModelLocator.DbContext.Context;
        public VisibilityMainControls VisibilityMainControls { get => _visibilityMainControls; }
        public DefencesAddViewModel Defences { get => _defences; }
        public MainUserInGuildAddViewModel Users { get => _users; }
        public UnitsAddViewModel Units { get => _units; }
        public WarsAddViewModel Wars { get => _wars; }
        public WarsDayAddViewModel WarsDay { get => _warsDay; }
        public RealStatAddViewModel RealStat { get => _realStat; }

        public ChartStatisticViewModel ChartStatistic { get => _chartStatistic; }
        public StatisticMainViewModel Statistic { get => _statistic;}

        public WarDayImportViewModel WarDayImport { get => _warDayImport; }
        #endregion

        public Visibility VisibilityMainGrid 
        { 
            get => _visibilityMainGrid; 
            set
            {
                _visibilityMainGrid = value;
                RaisePropertyChanged(nameof(VisibilityMainGrid));
            }
             
        }
       
        public ICommand OpenViewModel { get => _openViewModel; set => _openViewModel = value; }

        public ICommand SaveAs { get; set; }
        public ICommand Import { get; set; }
        
    }
}