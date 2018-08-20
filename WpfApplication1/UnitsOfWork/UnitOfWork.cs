using System;
using WpfApplication1.DataAccess;
using WpfApplication1.Models;
using WpfApplication1.Repositories;

namespace WpfApplication1.UnitsOfWork
{
    class UnitOfWork : IDisposable
    {
        private ApplicationContext _context = new ApplicationContext();

        private IGenericRepository<Usuario> _usuarioRepository;
        private IGenericRepository<Tag> _tagRepository;

        public IGenericRepository<Tag> TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new GenericRepository<Tag>(_context);
                }
                return _tagRepository;
            }
        }

        public IGenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (_usuarioRepository == null)
                {
                    _usuarioRepository = new GenericRepository<Usuario>(_context);
                }
                return _usuarioRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}