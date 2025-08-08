using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories
{
	public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenaricRepository(StoreContext DbContext)
        {
			_dbContext = DbContext;
		}

        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			//if (typeof(T) == typeof(Product))
			//{
			//	var RES = (IEnumerable<T>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
			//	return RES;
			//}
			return await _dbContext.Set<T>().ToListAsync();
		}


		public async Task<T?> GetByIdAsync(int id)
		{
			//if (typeof(T) == typeof(Product))
			//{
			//	return  _dbContext.Set<Product>().Where(P => P.id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefault() as T;
			//}

			 return await _dbContext.Set<T>().FindAsync(id);

		}


		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{

			return await ApplySpecifications(spec).AsNoTracking().ToListAsync();
		}



		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec )
		{

			var result =  await ApplySpecifications(spec).FirstOrDefaultAsync(spec.criteria);
			if (result is not null) 
			{
				return result;
			}

			return null;
		}

		public async Task<int> GetCountWithSpecAsync(ISpecifications<T> spec) 
		{


			return await ApplySpecifications(spec).CountAsync();
			
		

		
		}
		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec) 
		{

			return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);


		}

		 public async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);
			
		
	}
}
