﻿using Price_Calculator_Kata.Models;

namespace Price_Calculator_Kata.Services
{
    public class ReportGenerator : IReportGenerator
    {
        public IPriceCalculator priceCalculator { get; set; }

        public ReportGenerator(IPriceCalculator priceCalculator)
        {
            this.priceCalculator = priceCalculator;
        }

        public string reportPrice(Product product)
        {
            PriceBreakdown priceBreakdown = priceCalculator.CalculatePrice(product);

            List<string> reportList = new();

            reportList.Add($"Tax amount = ${priceBreakdown.Tax},");

            if (priceBreakdown.PreTaxDiscount != null)
            {
                reportList.Add($"Pre Tax Discount amount = ${priceBreakdown.PreTaxDiscount},");
            }

            if (priceBreakdown.TotalDiscount != null)
            {
                reportList.Add($"Total Discounts amount = ${priceBreakdown.TotalDiscount},");
            }

            if(priceBreakdown.AdditionalCostsResults.Count != 0)
            {
                foreach (var cost in priceBreakdown.AdditionalCostsResults)
                {
                    reportList.Add($"{cost.Name} Cost = ${cost.Cost},");
                }
            }

            reportList.Add($"Price before = ${priceBreakdown.ProductPrice},");

            reportList.Add($"price after = ${priceBreakdown.FinalPrice}");

            return string.Join(Environment.NewLine, reportList.ToArray());
        }
    }
}
