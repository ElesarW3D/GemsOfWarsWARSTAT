using GalaSoft.MvvmLight;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsWARSTAT.Services;
using System;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public abstract class BaseVisualViewModel : ViewModelBase, IDisposable
    {
        private WarDbContext _context;
        protected BaseVisualViewModel(WarDbContext dbContext)
        {
            _context = dbContext;
        }

        public WarDbContext Context 
        { 
            get => _context;
            set
            {
                if (value != _context)
                {
                    _context = value;
                    RaisePropertyChanged(nameof(Context));
                }
            }
        }
        public abstract void Load();

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DisposeAction();
            }
            // освобождаем неуправляемые объекты
            disposed = true;
        }

        protected virtual void DisposeAction()
        {
            Cleanup();
        }

        ~BaseVisualViewModel()
        {
            Dispose(false);
        }

        public abstract VisiblityControls ViewModelType { get; }
    }
}
