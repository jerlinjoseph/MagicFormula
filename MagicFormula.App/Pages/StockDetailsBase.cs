using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicFormula.Models;
using MagicFormula.Models.Data;
using MagicFormula.Job.Tasks;
using System.Collections;

namespace MagicFormula.App.Pages
{
    public class StockDetailsBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        public Stock stock { get; set; }

        public string GuruFocusUrl { get; set; }
        public string SeekingAlphaUrl { get; set; }

        public string MacroTrendsUrl { get; set; }

        public string  QuickFSUrl { get; set; }

        protected override Task OnInitializedAsync()
        {
            GetStock(Id);
            GuruFocusUrl = $"https://www.gurufocus.com/stock/{stock.Ticker}/summary";
            SeekingAlphaUrl = $"https://seekingalpha.com/symbol/{stock.Ticker}";
            MacroTrendsUrl = $"https://www.macrotrends.net/stocks/charts/{stock.Ticker}/revenue";
            QuickFSUrl = $"https://quickfs.net/company/{stock.Ticker}";

            return base.OnInitializedAsync();
        }
        private void GetStock(string id)
        {
            var dbContext = new MagicDbContext();
            MagicRepository repository = new MagicRepository(dbContext);
            stock = repository.GetStock(int.Parse(id));

        }



    }
}
