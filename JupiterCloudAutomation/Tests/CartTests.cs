using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JupiterCloudAutomation.Pages;
using System;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Tests
{
    [TestClass]
    public class CartTests : PageTest
    {
        [TestMethod]
        public async Task TestCase3_VerifyCartSubtotalPriceAndTotal()
        {
            try
            {
                await Page.GotoAsync("http://jupiter.cloud.planittesting.com");
                var homePage = new HomePage(Page);
                var shopPage = new ShopPage(Page);
                var cartPage = new CartPage(Page);

                await homePage.GotoShopPage();
                await shopPage.BuyItem("Stuffed Frog", 2);
                await shopPage.BuyItem("Fluffy Bunny", 5);
                await shopPage.BuyItem("Valentine Bear", 3);
                await shopPage.GotoCart();

                await cartPage.VerifySubtotal("Stuffed Frog", 2, 10.99);
                await cartPage.VerifySubtotal("Fluffy Bunny", 5, 9.99);
                await cartPage.VerifySubtotal("Valentine Bear", 3, 14.99);
                await cartPage.VerifyPrice("Stuffed Frog", 10.99);
                await cartPage.VerifyPrice("Fluffy Bunny", 9.99);
                await cartPage.VerifyPrice("Valentine Bear", 14.99);
                await cartPage.VerifyTotal();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TestCase3 failed: {ex.Message}");
                throw;
            }
        }
    }
}