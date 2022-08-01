namespace CMS.Blog.Repository.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly BlogContext _context;
        private DbSet<TEntity> entityEf;

        public RepositoryBase(BlogContext context)
        {
            _context = context;
            entityEf = context.Set<TEntity>();
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await entityEf.FindAsync(id);
        }

        public async Task<bool> Exist(Guid id) =>
            await entityEf.AnyAsync(record => record.Id == id);

        public async Task<IEnumerable<TEntity>> GetAll() =>
            await entityEf.ToListAsync();

        public async Task<TEntity> Create(TEntity entity, bool persist = true)
        {
            ValidateEntityIsNull(entity);

            await entityEf.AddAsync(entity);

            await PersistData(persist);

            return entity;
        }

        public async Task<bool> Update(TEntity entity, bool persist = true)
        {
            ValidateEntityIsNull(entity);

            entityEf.Update(entity);

            return await PersistData(persist);
        }

        public async Task<bool> Delete(TEntity entity, bool persist = true)
        {
            ValidateEntityIsNull(entity);

            entityEf.Remove(entity);

            return await PersistData(persist);
        }

        public ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);

            return ValueTask.CompletedTask;
        }

        internal void ValidateEntityIsNull(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
        }

        internal async Task<bool> PersistData(bool persist) =>
            persist ? (await _context.SaveChangesAsync() > 0) : false;

    }
}
