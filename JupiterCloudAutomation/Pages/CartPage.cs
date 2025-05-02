using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Pages
{
    public class CartPage
    {
        private readonly IPage _page;

        public CartPage(IPage page)
        {
            _page = page;
        }

        public async Task VerifySubtotal(string itemName, int expectedQuantity, double expectedPrice)
        {
            var subtotalLocator = _page.Locator($"//tr[td[contains(text(), '{itemName}')]]/td[4]");
            var subtotalText = await subtotalLocator.TextContentAsync();
            Console.WriteLine($"Subtotal text for {itemName}: '{subtotalText}'");

            if (string.IsNullOrWhiteSpace(subtotalText))
            {
                Assert.Fail($"Subtotal for {itemName} is empty or whitespace.");
            }

            var actualSubtotal = double.Parse(subtotalText!.Replace("$", "").Trim());
            var expectedSubtotal = expectedQuantity * expectedPrice;
            Assert.AreEqual(expectedSubtotal, actualSubtotal, 0.01, $"Subtotal for {itemName} is incorrect");
        }

        public async Task VerifyPrice(string itemName, double expectedPrice)
        {
            var priceLocator = _page.Locator($"//tr[td[contains(text(), '{itemName}')]]/td[2]");
            var priceText = await priceLocator.TextContentAsync();
            Console.WriteLine($"Price text for {itemName}: '{priceText}'");

            if (string.IsNullOrWhiteSpace(priceText))
            {
                Assert.Fail($"Price for {itemName} is empty or whitespace.");
            }

            var actualPrice = double.Parse(priceText!.Replace("$", "").Trim());
            Assert.AreEqual(expectedPrice, actualPrice, 0.01, $"Price for {itemName} is incorrect");
        }

        public async Task VerifyTotal()
        {
            var subtotalLocators = _page.Locator("//tr/td[4]");
            var subtotals = await subtotalLocators.AllTextContentsAsync();
            var totalLocator = _page.Locator("//strong[contains(text(), 'Total')]");
            var totalText = await totalLocator.TextContentAsync();
            Console.WriteLine($"Total text: '{totalText}'");

            if (string.IsNullOrWhiteSpace(totalText))
            {
                Assert.Fail("Total is empty or whitespace.");
            }

            var actualTotal = double.Parse(System.Text.RegularExpressions.Regex.Match(totalText!, @"[\d.]+").Value);
            var expectedTotal = subtotals
                .Select(s => double.Parse(s.Replace("$", "").Trim()))
                .Sum();
            Assert.AreEqual(expectedTotal, actualTotal, 0.01, "Total is incorrect");
        }
    }
}