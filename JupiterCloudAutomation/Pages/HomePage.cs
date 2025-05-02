using Microsoft.Playwright;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Pages
{
    public class HomePage
    {
        private readonly IPage _page;
        private readonly ILocator _contactLink;
        private readonly ILocator _shopLink;

        public HomePage(IPage page)
        {
            _page = page;
            _contactLink = page.Locator("a[href='#/contact']");
            _shopLink = page.GetByRole(AriaRole.Link, new() { Name = "Shop", Exact = true });
        }

        public async Task GotoContactPage()
        {
            await _contactLink.ClickAsync();
        }

        public async Task GotoShopPage()
        {
            await _shopLink.ClickAsync();
        }
    }
}