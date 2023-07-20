using Fiorella.Aplication.Abstraction.Repository;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fiorella.Persistence.Implementations.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;
    public DbSet<T> Table => _context.Set<T>();
    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)=> await Table.AddAsync(entity);

    public async Task AddRangeAsync(ICollection<T> entities)=>await Table.AddRangeAsync(entities);

    public void Remove(T entity)=>Table.Remove(entity);

    public void RemoveRange(ICollection<T> entities)=>Table.RemoveRange(entities);

    public async Task SaveChangeAsync()=>await _context.SaveChangesAsync();

    public void Update(T entity)=>Table.Update(entity);
}
