using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery , ISpecifications<TEntity> spec) 
		{

			var query = inputquery;

			if (spec.criteria is not null) 
			{
				query = query.Where(spec.criteria); 
				
		    }

			if (spec.OrderBy is not null) 
			{
				query = query.OrderBy(spec.OrderBy);
			
			}
			else if(spec.OrderByDesc is not null) 
			{
				query = query.OrderByDescending(spec.OrderByDesc);
			
			}

			if (spec.IsPagingEnabled)
			{
				query =query.Skip(spec.Skip).Take(spec.Take);

				
			}

			query = spec.includes.Aggregate(query, (Currentquery, Addingincludes) => Currentquery.Include(Addingincludes));

			//if (spec.Sort == "Name")
			//{
			//	query = spec.Sort.OrderBy(P => P.Name);
			//}

			return query;
		
		
		}
	}
}
