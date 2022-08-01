namespace CMS.Blog.Repository.Base
{
    public interface IRepositoryBase<TEntity> : IAsyncDisposable where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(Guid id);
        Task<bool> Exist(Guid id);
        Task<TEntity> Create(TEntity entity, bool persist = true);
        Task<bool> Update(TEntity entity, bool persist = true);
        Task<bool> Delete(TEntity entity, bool persist = true);
    }
}
