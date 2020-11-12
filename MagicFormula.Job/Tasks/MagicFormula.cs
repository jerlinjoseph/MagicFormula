using MagicFormula.Models;
using MagicFormula.Models.Data;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;


namespace MagicFormula.Job
{
    static class MagicFormula
    {

        public static void AccessMagicFormulaWebsite(IWebDriver driver, int numStocks)
        {
            ClearDatabase();

            var stockList50 = GenerateStockList(50, numStocks, driver);
            AddStocksToDatabase(stockList50, 50);

            var stockList100 = GenerateStockList(100, numStocks, driver);
            AddStocksToDatabase(stockList100, 100);

            var stockList250 = GenerateStockList(250, numStocks, driver);
            AddStocksToDatabase(stockList250, 250);

            var stockList500 = GenerateStockList(500, numStocks, driver);
            AddStocksToDatabase(stockList500, 500);

            var stockList1000 = GenerateStockList(1000, numStocks, driver);
            AddStocksToDatabase(stockList1000, 1000);

            var stockList2000 = GenerateStockList(2000, numStocks, driver);
            AddStocksToDatabase(stockList2000, 2000);

            var stockList3000 = GenerateStockList(3000, numStocks, driver);
            AddStocksToDatabase(stockList3000, 3000);

            var stockList4000 = GenerateStockList(4000, numStocks, driver);
            AddStocksToDatabase(stockList4000, 4000);

            var stockList5000 = GenerateStockList(5000, numStocks, driver);
            AddStocksToDatabase(stockList5000, 5000);


            RemoveDuplicates();

        }

        private static void RemoveDuplicates()
        {

            using (var dbContext = new MagicDbContext())
            {

                MagicRepository repository = new MagicRepository(dbContext);
                bool status = repository.RemoveDuplicates();


                Console.WriteLine("Duplicates Removed: " + status);
            }


        }

        private static void ClearDatabase()
        {

            using (var dbContext = new MagicDbContext())
            {

                MagicRepository repository = new MagicRepository(dbContext);
                bool status = repository.ClearDatabase();


                Console.WriteLine("Database Cleared: " + status);
            }


        }

        private static void AddStocksToDatabase(List<Stock> stocks, int marketCap)
        {

            using (var dbContext = new MagicDbContext())
            {

                MagicRepository repository = new MagicRepository(dbContext);
                foreach (Stock stock in stocks)
                {
                    stock.MagicFormulaMarketCap = marketCap;
                    repository.Add(stock);

                }

                bool b = repository.Save();
                Console.WriteLine("Save status" + b);
            }

        }

        public static List<Stock> GenerateStockList(int minMarketCap, int numStocks, IWebDriver driver)
        {
            System.Threading.Thread.Sleep(1000);

            IWebElement marketCap = driver.FindElement(By.Id("MinimumMarketCap"));
            marketCap.Clear();
            marketCap.SendKeys(minMarketCap.ToString());

            IWebElement numStockRadioButton;

            if (numStocks == 30)
            {
                numStockRadioButton = driver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[1]/div[2]/div[2]/div/div/div/div/div/div/div/form/table/tbody/tr[4]/td[2]/table/tbody/tr[1]/td[1]/span[1]/input"));
                numStockRadioButton.Click();
            }
            else
            {
                numStockRadioButton = driver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[1]/div[2]/div[2]/div/div/div/div/div/div/div/form/table/tbody/tr[4]/td[2]/table/tbody/tr[1]/td[3]/span/input"));
                numStockRadioButton.Click();

            }

            driver.FindElement(By.Id("stocks")).Click();

            System.Threading.Thread.Sleep(1000);

            List<Stock> stockList = new List<Stock>();

            for (int i = 1; i <= numStocks; i++)
            {
                String ticker = driver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[1]/div[2]/div[3]/div[1]/div/div/div/div/div/div/div/div/div/table/tbody/tr[{i}]/td[2]")).Text;
                Stock stock = new Stock();
                stock.Ticker = ticker;

                stockList.Add(stock);

            }

            return stockList;

        }

        public static void MagicFormulaWebsite_Logon(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(@"https://www.magicformulainvesting.com/Account/LogOn");

            driver.FindElement(By.Id("Email")).SendKeys("jerlin.urbanecho@gmail.com");
            driver.FindElement(By.Id("Password")).SendKeys("Stranger99");

            IWebElement login = driver.FindElement(By.Id("login"));
            login.Click();

        }
    }
}
