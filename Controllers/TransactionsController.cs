using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewComponents;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
	[Authorize]
	public class TransactionsController : Controller
	{
        private readonly TransactionSQLRepository transactionsRepository;

        public TransactionsController(TransactionSQLRepository transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        public IActionResult Index()
		{
			TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
			return View(transactionsViewModel);
		}

		public IActionResult Search(TransactionsViewModel transactionsViewModel) 
		{
			var transactions= transactionsRepository.Search(
				transactionsViewModel.CashierName??string.Empty, 
				transactionsViewModel.StartDate, 
				transactionsViewModel.EndDate);

			transactionsViewModel.Transactions = transactions;

			return View("Index", transactionsViewModel);
		}
	}
}
