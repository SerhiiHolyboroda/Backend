using Abackend.Data;
using Core.Interface;
using Core.Models;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GenericImplementation {
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {

        public readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) {
             _dbContext = dbContext;
        }

        public async Task<int> CountAsync(ISpecification<T> spec) {
            return await ApplySpecifcation(spec).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id) {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) {
            return await ApplySpecifcation(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync() {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) {
            return await ApplySpecifcation(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecifcation(ISpecification<T> spec) {

            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec );
        }
    }
}
