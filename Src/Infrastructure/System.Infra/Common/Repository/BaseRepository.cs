using System.Domain.Common.Entities;
using System.Domain.Common.Repositories;
using System.Infra.Contexts;

namespace System.Infra.Common.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
    {
        private readonly SystemDbContext _context;

        public BaseRepository(SystemDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(long id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            entity.IsDelete = true;
            Update(entity);
        }

        public TEntity GetById(long id)
        {
            return _context.Set<TEntity>().FirstOrDefault(t => t.Id == id);
        }

        public List<TEntity> GetList()
        {
            return _context.Set<TEntity>().ToList();
        }

        public async void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();

        }

        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CheckAvailability(long id)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(t => t.Id == id);
            if (entity is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Close()
        {
            _context.Dispose();
        }
    }
}
