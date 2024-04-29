using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context) {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey? id)
           => await _context.Set<TEntity>().FindAsync(id);
        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs)
          => await ApplySpecification(specs).FirstOrDefaultAsync();


        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).ToListAsync();
        public async Task<int> CountWithSpecificationAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).CountAsync();
        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public async void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specs)
         => SpecificationEvaluater<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs);

      
    }
}