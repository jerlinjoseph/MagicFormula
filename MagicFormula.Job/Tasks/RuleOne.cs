using MagicFormula.Models;
using MagicFormula.Models.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace MagicFormula.Job.Tasks
{
    class RuleOne
    {
        //Get data for All stocks from the website or scrape based on a single ticker
        public static void AccessRuleOneWebsite(IWebDriver driver, Stock stock)
        {
            Console.WriteLine("*****Retrieving data for......" + stock.Ticker);
            System.Threading.Thread.Sleep(1000);
            try
            {
                ScrapeData(driver, stock);
                stock.RuleOneUpdateStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{stock.Ticker} did not update correctly");
            }
        
        }

        private static void ScrapeData(IWebDriver driver, Stock stock)
        {
            try {
                var txtBox = driver.FindElement(By.Id("stock_value"));
                txtBox.Clear();
                txtBox.SendKeys(stock.Ticker);
                IWebElement goButton = driver.FindElement(By.Id("btnGo"));
                goButton.Click();

                //Wait for elements to load
                System.Threading.Thread.Sleep(1000);

                IWebElement ruleOneNumbers = driver.FindElement(By.Id("townAnalysis-tab"));
                ruleOneNumbers.Click();
                System.Threading.Thread.Sleep(1000);

                //get information from the Rule One Numbers page
                string companyText = driver.FindElement(By.Id("ctl00_ctl00_ctl00_MainContent_MainContent_quoteDetail_lbl_Company")).Text;
                String[] company = companyText.Split('(', 2);
                stock.Company = company[0].Trim();

                string text = driver.FindElement(By.Id("td22")).Text;
                String[] debtInYears = text.Split(' ', 2);
                stock.DebtInYears = Decimal.TryParse(debtInYears[0].Trim(), out decimal result1) ? result1 : -1;

                string grossMargin = FindElementValueFromTable(driver, "tblOperatingRatios", "Gross");
                stock.GrossMargin = Decimal.TryParse(grossMargin, out decimal result2) ? result2 : -1;

                string netMargin = FindElementValueFromTable(driver, "tblOperatingRatios", "Profit Margin");
                stock.NetMargin = Decimal.TryParse(netMargin, out decimal result3) ? result3 : -1;

                string returnOnEquity = FindElementValueFromTable(driver, "tblCapitalRatios", "Return on Equity");
                stock.ReturnOnEquity = Decimal.TryParse(returnOnEquity, out decimal result4) ? result4 : -1;

                string returnOnInvestedCapital = driver.FindElement(By.Id("tdROIC1")).Text;
                stock.ReturnOnInvestedCapital = Decimal.TryParse(returnOnInvestedCapital.Substring(0, (returnOnInvestedCapital.Length - 1)), out decimal result5) ? result5 : -1;
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

        public static void RuleOneWebsite_Logon(IWebDriver driver,string username,string password)
        {
            driver.Navigate().GoToUrl(@"https://toolbox.ruleoneinvesting.com/");
            System.Threading.Thread.Sleep(1000);

            
            driver.FindElement(By.Id("ctl00_ContentBody_inputEmail")).SendKeys(username);
            driver.FindElement(By.Id("ctl00_ContentBody_inputPassword")).SendKeys(password);

            IWebElement login = driver.FindElement(By.ClassName("orange-button"));
            login.Click();

        }

        //caled by Job
        public static void Start(IWebDriver driver, Stock stock)
        {
           
            
            RuleOne.AccessRuleOneWebsite(driver, stock);            
            Console.WriteLine("*****Completed Rule One ......");

        }

        
    }
}
