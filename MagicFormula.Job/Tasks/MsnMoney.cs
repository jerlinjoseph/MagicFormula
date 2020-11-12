using MagicFormula.Models;
using MagicFormula.Models.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicFormula.Job.Tasks
{
    public class MsnMoney
    {
        //Get data for All stocks from the website or scrape based on a single ticker
        public static void AccessMsnWebsite(IWebDriver driver, Stock stock)
        {
            Console.WriteLine("*****Retrieving data for......" + stock.Ticker);
            System.Threading.Thread.Sleep(1000);
            try
            {
                ScrapeData(driver, stock);
                stock.MsnUpdateStatus = true;
            }
            catch(Exception e)
            {
                Console.WriteLine($"{stock.Ticker} did not update correctly");
                Console.WriteLine($"Error: {e}");
            }
            
        }

        private static void ScrapeData(IWebDriver driver, Stock stock)
        {
            try
            {
                driver.Navigate().GoToUrl(@"https://www.msn.com/en-us/money");
                System.Threading.Thread.Sleep(2000);

                driver.FindElement(By.ClassName(@"autoSuggest-DS-EntryPoint1-1")).SendKeys(stock.Ticker);

                IWebElement search = driver.FindElement(By.ClassName("searchIcon-DS-EntryPoint1-1"));
                System.Threading.Thread.Sleep(2000);
                search.Click();
                System.Threading.Thread.Sleep(2000);

                IWebElement companytab = driver.FindElement(By.Id("profile"));
                IWebElement child = companytab.FindElement(By.TagName("a"));
                child.Click();
                System.Threading.Thread.Sleep(10000);

                IWebElement description = driver.FindElement(By.ClassName(@"description-area"));
                stock.CompanyDescription = description.Text;

                stock.Sector = driver.FindElement(By.ClassName(@"captionData")).Text;
            }
            catch(Exception e)
            {
                throw;
            }

            

        }

        public static void Start(IWebDriver driver,Stock stock)
        {
            Console.WriteLine("*****Started MSN ......");
            
            AccessMsnWebsite(driver, stock);
            
            

        }
    }
}
