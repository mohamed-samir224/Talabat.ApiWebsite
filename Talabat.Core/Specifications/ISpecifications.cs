using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{

		public Expression<Func<T, bool>>? criteria { get; set; }  //p => p.id == id

		public List<Expression<Func<T, object>>> includes { get; set; } //p=>p.Navigational prop && return type is any thing list of item to one item

		public Expression<Func<T,object>> OrderBy { get; set; }
		public Expression<Func<T,object>> OrderByDesc { get; set; }	

		public int Skip { get; set; } 
		public int Take { get; set; }

		public bool IsPagingEnabled { get; set; } // to check if we need to apply paging or not


	}
	
	
}
