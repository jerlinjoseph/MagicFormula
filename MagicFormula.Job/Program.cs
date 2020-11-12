using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MagicFormula.Models;
using MagicFormula.Job.Tasks;
using MagicFormula.Models.Data;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace MagicFormula.Job
{
    class Program
    {
        static void Main(string[] args)
        {
           /* var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

           */

            Console.WriteLine("Select your option");
            Console.WriteLine("1.Get Magic Formula Stocks");
            Console.WriteLine("2.Get MSN Data");
            Console.WriteLine("3.Get Rule One Data");
            Console.WriteLine("4.Get Guru Focus Data");
            Console.Write("Enter Option:");

            string option = Console.ReadLine();

            switch(Int32.Parse(option))
            {
                case 1: GetMagicFormulaStocks();
                        break;
                case 2: GetDataFromMSN();
                        break;
                case 3: Console.WriteLine("Enter Rule One Username");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Rule One Username");
                        string password = Console.ReadLine();
                
                        GetDataFromRuleOne(username,password);
                        
                        break;
                case 4: GetDataFromGuruFocus();
                        break;
                default: Console.WriteLine("Wrong option");
                         break;

            }
            
        }

        static void GetDataFromMSN()
        {
            Console.WriteLine("*****Started MSN ......");
            IWebDriver driver = new ChromeDriver();
            
            using (var dbContext = new MagicDbContext())
            {
                MagicRepository repository = new MagicRepository(dbContext);
                var stocks = repository.GetMsnStocks();
                foreach (Stock stock in stocks)
                {
                    MsnMoney.Start(driver,stock);
                    repository.Save();
                }
            }

            driver.Close();
            driver.Quit();

        }

        static void GetDataFromRuleOne(string username,string password)
        {
            Console.WriteLine("*****Started Rule One ......");
            IWebDriver driver = new ChromeDriver();

            RuleOne.RuleOneWebsite_Logon(driver,username,password);
            System.Threading.Thread.Sleep(1000);

            using (var dbContext = new MagicDbContext())
            {
                MagicRepository repository = new MagicRepository(dbContext);
                var stocks = repository.GetRuleOneStocks();
                foreach (Stock stock in stocks)
                {
                    RuleOne.Start(driver,stock);
                    repository.Save();
                }
            }

            driver.Close();
            driver.Quit();

        }

        static void GetDataFromGuruFocus()
        {
            Console.WriteLine("*****Started Rule One ......");
            IWebDriver driver = new ChromeDriver();

            using (var dbContext = new MagicDbContext())
            {
                MagicRepository repository = new MagicRepository(dbContext);
                var stocks = repository.GetGuruFocusStocks();
                foreach (Stock stock in stocks)
                {
                    GuruFocus.Start(driver, stock);
                    repository.Save();
                }
            }
            driver.Close();
            driver.Quit();

        }

       

        static List<Stock> GetMagicFormulaStocks()
        {
            Console.WriteLine("*****Started Magic Formula ......");
            IWebDriver driver = new ChromeDriver();
            List<String> stockListCombined = new List<string>();

            const int NUMBER_OF_STOCKS = 50;

            MagicFormula.MagicFormulaWebsite_Logon(driver);

            MagicFormula.AccessMagicFormulaWebsite(driver, NUMBER_OF_STOCKS);


            Console.WriteLine("\n\nCombined List .....");
            stockListCombined.ForEach(i => Console.Write("{0}\t", i));


            List<String> noDupesList = stockListCombined.Distinct().ToList();
            noDupesList.Sort();
            Console.WriteLine("\n\n");
            Console.WriteLine($"Stock List without duplicates has {noDupesList.Count} stocks");
            noDupesList.ForEach(i => Console.Write("{0}\t", i));


            List<Stock> stocks = new List<Stock>();

            foreach (string record in noDupesList)
            {
                Stock stock = new Stock();
                stock.Ticker = record;
                stocks.Add(stock);

            }

            driver.Close();
            driver.Quit();
            Console.WriteLine("*****Completed Magic Formula ......");
            return stocks;
            
        }

   


    }
}
