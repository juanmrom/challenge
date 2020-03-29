using challenge.DAL.Entity;
using challenge.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace challenge.DAL.CustomRepository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ChallengeContext context)
            : base(context)
        {

        }

        public override PagedResult<Payment> Get(int currentPage, int pageSize, Func<Payment, bool> expression)
        {            
            pageSize = pageSize == 0 ? 10 : pageSize;
            currentPage = currentPage == 0 ? 1 : currentPage;
            var result = new PagedResult<Payment>();
            result.CurrentPage = currentPage;
            result.PageSize = pageSize;                        

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (currentPage - 1) * pageSize;

            result.Results = _context.Payments.Include(p => p.PaymentType).Where(expression).Skip(skip).Take(pageSize).ToList();

            result.RowCount = _context.Payments.Count(expression);

            return result;
        }
    }
}
