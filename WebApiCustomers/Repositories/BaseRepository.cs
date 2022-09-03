using Microsoft.EntityFrameworkCore;
using WebApiCustomers.Data;

namespace WebApiCustomers.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly CustomerDemoDbContext _context;
    private readonly DbSet<TEntity> _dbset;
    public BaseRepository(CustomerDemoDbContext context)
    {
        _context = context;
        _dbset = _context.Set<TEntity>();
    }
    public async Task AddAsync(TEntity entity) => await _dbset.AddAsync(entity);
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbset.ToListAsync();
    public async Task<TEntity?> GetAsync(int? id) => await _dbset.FindAsync(id);
    public async Task DeleteAsync(int id)
    {
        var dataToDelete = await _dbset.FindAsync(id);
        if(dataToDelete!=null)
        _dbset.Remove(dataToDelete);
    }
    public async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(()=> _dbset.Attach(entity));
        _context.Entry(entity).State = EntityState.Modified;
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}

