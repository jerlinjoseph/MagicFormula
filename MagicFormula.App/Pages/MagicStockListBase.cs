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
    public class MagicStockListBase : ComponentBase
    {
        public  IEnumerable<Stock> stocks { get; set; }
        
        protected override Task OnInitializedAsync()
        {
            LoadAllStocks();
            return base.OnInitializedAsync();
        }
        private void LoadAllStocks()
        {
            var dbContext = new MagicDbContext();
            MagicRepository repository = new MagicRepository(dbContext);
            stocks = repository.GetAllStocks();
            
        }

       /* protected void UpdateTicker(string ticker)
        {
            MsnMoney.Start(ticker);

        }*/

    }
}
