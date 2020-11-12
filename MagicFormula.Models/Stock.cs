using System;

namespace MagicFormula.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Ticker { get; set; } //Magic Formula
        public string Company { get; set; } //Rule One

        public int MagicFormulaMarketCap { get; set; }

        public string Sector { get; set; } //MSN

        public decimal DividendYield { get; set; } //Guru Focus

        public decimal PayoutRatio { get; set; } //Guru Focus

        public decimal PriceToEarning { get; set; } //Guru Focus

        public decimal PriceToBook { get; set; } //Guru Focus

        public decimal ReturnOnEquity { get; set; } //Guru Focus

        public decimal ReturnOnInvestedCapital { get; set; } //Guru Focus

        public decimal ROC_Greenblatt { get; set; } //Guru Focus

        public decimal EarningsYield { get; set; } //Guru Focus

        public decimal CashToDebt { get; set; } //Guru Focus

        public decimal InterestCoverage { get; set; } //Guru Focus

        public decimal EarningsPerShare { get; set; } //Guru Focus

        public decimal PiotroskiScore { get; set; } //Guru Focus

        public decimal DebtInYears { get; set; } //Rule One

        public decimal GrossMargin { get; set; } //Rule One

        public decimal NetMargin { get; set; } //Rule One

        public string CompanyDescription { get; set; } //MSN

        public bool MsnUpdateStatus { get; set; }

        public bool RuleOneUpdateStatus { get; set; }

        public bool GuruFocusUpdateStatus { get; set; }
    }
}
