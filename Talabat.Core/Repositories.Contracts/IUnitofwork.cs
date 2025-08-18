using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contracts
{
	public interface IUnitofwork : IAsyncDisposable 
	{
		public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

		public  Task<int> Complete();


	}
}
