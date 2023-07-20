using Fiorella.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fiorella.Aplication.Abstraction.Repository;

public interface IRepository<T> where T : BaseEntity,new()
{
    public DbSet<T> Table { get;}
}
