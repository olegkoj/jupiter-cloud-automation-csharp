using Microsoft.Playwright;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Pages
{
    public class ShopPage
    {
        private readonly IPage _page;

        public ShopPage(IPage page)
        {
            _page = page;
        }

        public async Task BuyItem(string itemName, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                await _page.Locator($"//h4[text()='{itemName}']/..//a[text()='Buy']").ClickAsync();
            }
        }

        public async Task GotoCart()
        {
            await _page.Locator("a[href='#/cart']").ClickAsync();
        }
    }
}