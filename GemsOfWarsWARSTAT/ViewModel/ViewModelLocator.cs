using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GemsOfWarsWARSTAT.Services;
using SimpleControlsLibrary.Dialogs.DialogFacade;

namespace GemsOfWarsWARSTAT.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new MainViewModel(), nameof(MainViewModel));
            SimpleIoc.Default.Register(() => new WarDbContextService(), nameof(WarDbContextService));
            SimpleIoc.Default.Register(() => new VisibilityMainControls(), nameof(VisibilityMainControls));
            SimpleIoc.Default.Register(() => new DialogFacade(), nameof(DialogFacade)); 
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>(nameof(MainViewModel));
            }
        }

        public DialogFacade DialogFacade
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DialogFacade>(nameof(DialogFacade));
            }
        }

        public WarDbContextService DbContext
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WarDbContextService>(nameof(WarDbContextService));
            }
        }

        public VisibilityMainControls VisibilityMainControls
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VisibilityMainControls>(nameof(VisibilityMainControls));
            }
        }


        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Unregister<WarDbContextService>();
            SimpleIoc.Default.Unregister<VisibilityMainControls>();
            SimpleIoc.Default.Unregister<OneDataGridVisibility>();
            SimpleIoc.Default.Unregister<DialogFacade>();
        }
    }
}