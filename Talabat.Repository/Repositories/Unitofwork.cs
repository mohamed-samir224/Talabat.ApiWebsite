using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories
{
	public class Unitofwork : IUnitofwork
	{
		private readonly StoreContext _dbcontext;
		private  Hashtable  _repos ;

		public Unitofwork(StoreContext dbcontext)
		{
			_dbcontext = dbcontext;
			_repos = new Hashtable();
		}

		public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var type = typeof(TEntity).Name;
			if (!_repos.ContainsKey(type))
			{
				var reposatory = new GenaricRepository<TEntity>(_dbcontext);
				_repos.Add(type, reposatory);
			}
			return _repos[type] as IGenaricRepository<TEntity>;	

		}
		public async Task<int> Complete()
		
			 => await _dbcontext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
		=> await _dbcontext.DisposeAsync();
	}
}
