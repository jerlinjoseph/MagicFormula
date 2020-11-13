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
    public class TopStockBase : ComponentBase
    {
        public IEnumerable<Stock> stocks { get; set; }

        protected override Task OnInitializedAsync()
        {
            LoadTopStocks();
            return base.OnInitializedAsync();
        }
        private void LoadTopStocks()
        {
            var dbContext = new MagicDbContext();
            MagicRepository repository = new MagicRepository(dbContext);
            stocks = repository.GetTopStocks();

        }

        /* protected void UpdateTicker(string ticker)
         {
             MsnMoney.Start(ticker);

         }*/

    }
}
