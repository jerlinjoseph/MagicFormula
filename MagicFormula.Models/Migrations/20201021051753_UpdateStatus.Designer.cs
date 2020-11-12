﻿// <auto-generated />
using MagicFormula.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagicFormula.Models.Migrations
{
    [DbContext(typeof(MagicDbContext))]
    [Migration("20201021051753_UpdateStatus")]
    partial class UpdateStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("MagicFormula.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CashToDebt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyDescription")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DebtInYears")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DividendYield")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("EarningsPerShare")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("EarningsYield")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("GrossMargin")
                        .HasColumnType("TEXT");

                    b.Property<bool>("GuruFocusUpdateStatus")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("InterestCoverage")
                        .HasColumnType("TEXT");

                    b.Property<int>("MagicFormulaMarketCap")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MsnUpdateStatus")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("NetMargin")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PayoutRatio")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PiotroskiScore")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PriceToBook")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PriceToEarning")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ROC_Greenblatt")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ReturnOnEquity")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ReturnOnInvestedCapital")
                        .HasColumnType("TEXT");

                    b.Property<bool>("RuleOneUpdateStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sector")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ticker")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
