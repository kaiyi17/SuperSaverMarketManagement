using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.ViewComponents
{
	[ViewComponent]
	public class TransactionsViewComponent :ViewComponent
	{
        private readonly TransactionSQLRepository transactionsRepository;

        public TransactionsViewComponent(TransactionSQLRepository transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        public IViewComponentResult Invoke(string userName)
		{
			var transactions = transactionsRepository.GetByDayAndCashier(userName, DateTime.Now);
			return View(transactions);
		}
	}
}
