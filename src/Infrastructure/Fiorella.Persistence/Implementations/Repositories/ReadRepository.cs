using Fiorella.Aplication.Abstraction.Repository;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Fiorella.Persistence.Implementations.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;
    public DbSet<T> Table => _context.Set<T>();
    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll(bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        foreach (var include in includes)
        {
            query=query.Include(include);
        }
        return isTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression,
                                          int take,
                                          int skip,
                                          bool isTracking = true,
                                          params string[] includes)
    {
        var query = Table.Where(expression).Skip(skip).Take(take).AsQueryable();
        foreach (var include in includes)
        {
            query=query.Include(include);
        }
        return isTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression,
                                                 int take,
                                                 int skip,
                                                 Expression<Func<T, object>> expressionOrder,
                                                 bool isOrdered = true,
                                                 bool isTracking = true,
                                                 params string[] includes)
    {
        var query = Table.Where(expression).AsQueryable();
        query = isOrdered ? query.OrderBy(expressionOrder) : query.OrderByDescending(expressionOrder);
        query = query.Skip(skip).Take(take);
        foreach (var include in includes)
        {
            query=query.Include(include);
        }
        return isTracking ? query : query.AsNoTracking();
    }

    public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
    {
        var query = isTracking ? Table : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<T?> GetByIdAsync(int Id)=> await Table.FindAsync(Id);
}
