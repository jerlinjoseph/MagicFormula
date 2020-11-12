using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicFormula.Models.Data
{
    public class MagicRepository

    {
        private readonly MagicDbContext _context;
        public MagicRepository(MagicDbContext context)
        {
            _context = context;
        }



        public void Add(Stock s)
        {
            _context.Stocks.Add(s);
        }

        public void AddAll(List<Stock> stocks)
        {
            foreach (Stock stock in stocks)
            {

                _context.Stocks.Add(stock);

            }

        }

        public void Delete(Stock s)
        {
            _context.Stocks.Remove(s);
        }

        public Stock GetStock(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(p => p.Id == id);

            return stock;
        }

        public Stock FindStock(string ticker)
        {
            var stock = _context.Stocks.FirstOrDefault(p => p.Ticker == ticker);
            return stock;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            var stocks = _context.Stocks.OrderBy(s => s.Ticker);
            //.Where(p => p.Company == null);
            return stocks;
        }

        public IEnumerable<Stock> GetTopStocks()
        {
            var stocks = _context.Stocks
            .Where(p => (int)p.EarningsYield > 8)
            .Where(p => (int)p.InterestCoverage > 5)
            .Where(p => (int)p.DebtInYears < 3)
            .OrderBy(p => p.Ticker);

            return stocks;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool RemoveDuplicates()
        {
            return _context.Database.ExecuteSqlCommand(@"delete from Stocks where ROWID not in (select min(ROWID) from Stocks group by ticker) ") > 0;

        }

        public bool ClearDatabase()
        {
            return _context.Database.ExecuteSqlCommand("DELETE FROM Stocks") > 0;
        }

        public IEnumerable<Stock> GetMsnStocks()
        {
            var stocks = _context.Stocks.OrderBy(s => s.Ticker)
            .Where(p => p.MsnUpdateStatus == false);
            return stocks;

        }
        public IEnumerable<Stock> GetRuleOneStocks()
        {
            var stocks = _context.Stocks.OrderBy(s => s.Ticker)
            .Where(p => p.RuleOneUpdateStatus == false);
            return stocks;

        }

        public IEnumerable<Stock> GetGuruFocusStocks()
        {
            var stocks = _context.Stocks.OrderBy(s => s.Ticker)
            .Where(p => p.GuruFocusUpdateStatus == false);
            return stocks;

        }

    }
}
