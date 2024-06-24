using GalaSoft.MvvmLight;
using GemsOfWarsWARSTAT.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsWARSTAT.Services
{
    public class WarDbContextService : IWarDbContextService
    {
        private WarDbContext _context;
        public WarDbContext Context { get => _context; set => _context = value; }
        public WarDbContextService()
        {
            _context = new WarDbContext();
        }

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
           
        }

        public void Cleanup()
        {
            Dispose();
        }
    }
}
