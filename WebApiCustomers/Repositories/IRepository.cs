namespace WebApiCustomers.Repositories;
public interface IRepository<TEntity>

{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(int? id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}


