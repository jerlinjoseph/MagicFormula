using MagicFormula.Models;
using MagicFormula.Models.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace MagicFormula.Job.Tasks
{
    class GuruFocus
    {
        public static void AccessGuruFocusWebsite(IWebDriver driver, Stock stock)
        {

            Console.WriteLine("*****Retrieving data for......" + stock.Ticker);
            System.Threading.Thread.Sleep(1000);
            try
            {
                ScrapeData(driver, stock);
                stock.GuruFocusUpdateStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{stock.Ticker} did not update correctly");
            }


        }

        private static void ScrapeData(IWebDriver driver, Stock stock)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                driver.Navigate().GoToUrl($"https://www.gurufocus.com/stock/{stock.Ticker}/summary");

                string CashToDebt = FindElementValueFromTable(driver, "financial-strength", "Cash");
                stock.CashToDebt = Decimal.TryParse(CashToDebt, out decimal result) ? result : -1;

                string piotroskiScore = FindElementValueFromTable(driver, "financial-strength", "Piotroski");
                stock.PiotroskiScore = Decimal.TryParse(piotroskiScore, out decimal result1) ? result1 : -1;

                string dividendYield = FindElementValueFromTable(driver, "dividend", "Dividend Yield");
                stock.DividendYield = Decimal.TryParse(dividendYield, out decimal result2) ? result2 : -1;

                string payoutRatio = FindElementValueFromTable(driver, "dividend", "Dividend Payout Ratio");
                stock.PayoutRatio = Decimal.TryParse(payoutRatio, out decimal result3) ? result3 : -1;

                string priceToEarning = FindElementValueFromTable(driver, "ratios", "PE Ratio");
                stock.PriceToEarning = Decimal.TryParse(priceToEarning, out decimal result4) ? result4 : -1;

                string priceToBook = FindElementValueFromTable(driver, "ratios", "PB Ratio");
                stock.PriceToBook = Decimal.TryParse(priceToBook, out decimal result5) ? result5 : -1;

                //string returnOnEquity = FindElementValueFromTable(driver, "profitability", "ROE");
                //stock.ReturnOnEquity = Decimal.TryParse(returnOnEquity, out decimal result6) ? result6 : -1;


                string rOC_Greenblatt = FindElementValueFromTable(driver, "profitability", "ROC");
                stock.ROC_Greenblatt = Decimal.TryParse(rOC_Greenblatt, out decimal result8) ? result8 : -1;

                string earningsYield = FindElementValueFromTable(driver, "valuation", "Earnings Yield");
                stock.EarningsYield = Decimal.TryParse(earningsYield, out decimal result9) ? result9 : -1;

                string interestCoverage = FindElementValueFromTable(driver, "financial-strength", "Interest Coverage");
                stock.InterestCoverage = Decimal.TryParse(interestCoverage, out decimal result10) ? result10 : -1;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        private static string FindElementValueFromTable(IWebDriver driver, string tableId, string valueToFind)
        {
            //find the table div by id and get all the rows
            IReadOnlyList<IWebElement> rows = driver.FindElement(By.Id(tableId)).FindElements(By.TagName("tr"));

            string colValue = "";
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> columns = row.FindElements(By.TagName("td"));

                foreach (IWebElement column in columns)
                {
                    string colName = column.Text;

                    if (colName.StartsWith(valueToFind))
                    {
                        colValue = columns[1].Text; //values are in the second column
                        return colValue;
                    }
                }

            }
            Console.WriteLine("Could not find " + valueToFind);
            return colValue;

        }

        public static void Start(IWebDriver driver, Stock stock)
        {
            
            GuruFocus.AccessGuruFocusWebsite(driver, stock);
            

        }

    }
}
