using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly ApplicationContext _DbContext;
        protected readonly DbSet<TEntity> _DbSet;
        protected Repository(ApplicationContext context)
        {
            _DbContext = context;
            _DbSet = _DbContext.Set<TEntity>();
        }

        public async Task Adicionar(TEntity entity)
        {
            _DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            _DbSet.Update(entity);
            await SaveChanges();
        }

        public void Dispose()
        {
            _DbContext?.Dispose();
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task Remover(int id)
        {
            _DbSet.Remove(new TEntity {Id = id });
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await _DbContext.SaveChangesAsync();
        }
    }
}
