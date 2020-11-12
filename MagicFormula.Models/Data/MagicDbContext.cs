using Microsoft.EntityFrameworkCore;


namespace MagicFormula.Models.Data
{
    public class MagicDbContext : DbContext
    {

        public DbSet<Stock> Stocks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=//media//jerlin//Workspace//Workspace//MagicFormula//magicformula.db");
    }
}
