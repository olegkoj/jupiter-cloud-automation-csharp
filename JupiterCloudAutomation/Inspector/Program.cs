using System;
using Microsoft.Playwright;

namespace Inspector
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("http://jupiter.cloud.planittesting.com/#/contact");
            Console.WriteLine("Page loaded. Use Playwright Inspector to inspect DOM.");
            await page.WaitForTimeoutAsync(30000); // Keep browser open for inspection
        }
    }
}