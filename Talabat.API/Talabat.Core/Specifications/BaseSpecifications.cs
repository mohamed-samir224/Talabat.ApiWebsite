using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? criteria { get; set; }
        public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>();  // List Cant be Null!!!!!!!!!!!!!
		public string Sort { get ; set; }
		public Expression<Func<T, object>> OrderBy { get; set  ; }
		public Expression<Func<T, object>> OrderByDesc { get  ; set; }
		public int Skip { get ; set; }
		public int Take { get ; set; }
		public bool IsPagingEnabled { get; set ; }

		public BaseSpecifications()
		{
				
		}
        public BaseSpecifications(Expression<Func<T, bool>> criteriaexpression)
        {
            criteria = criteriaexpression;

        }
		//public void AddOrderBy()

		public void ApplayPagination(int? skip, int? take) 
		{

			IsPagingEnabled = true;

			Skip = skip?? 0; // if skip is null then set it to 0
			Take = take?? 5; // if take is null then set it to 10


		}


	}
}
