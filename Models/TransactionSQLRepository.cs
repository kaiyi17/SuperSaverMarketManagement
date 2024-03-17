using Microsoft.EntityFrameworkCore;


namespace WebApp.Models
{
	public class TransactionSQLRepository
	{

		private readonly MarketContext db;

		public TransactionSQLRepository(MarketContext db)
		{
			this.db = db;
		}


		public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
		{
			if (string.IsNullOrWhiteSpace(cashierName))
				return db.Transactions.Where(x => x.TimeStamp.Date == date.Date);
			else
				return db.Transactions.Where(x =>
					EF.Functions.Like(x.CashierName, $"%{cashierName}%") && x.TimeStamp.Date == date.Date);
		}

		public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
		{
			if (string.IsNullOrWhiteSpace(cashierName))
				return db.Transactions.Where(x => 
					x.TimeStamp.Date >= startDate.Date 
					&& x.TimeStamp.Date <= endDate.Date);
			else
				return db.Transactions.Where(x =>
					EF.Functions.Like(x.CashierName, $"%{cashierName}%") && 
					x.TimeStamp.Date >= startDate.Date && 
					x.TimeStamp.Date <= endDate.Date);
		}

		public void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
		{
			var transaction = new Transaction
			{
				ProductId = productId,
				ProductName = productName,
				TimeStamp = DateTime.Now,
				Price = price,
				BeforeQty = beforeQty,
				SoldQty = soldQty,
				CashierName = cashierName
			};

			db.Transactions.Add(transaction);
			db.SaveChanges();
		}

	}
}

