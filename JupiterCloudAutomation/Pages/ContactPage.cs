using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Pages
{
    public class ContactPage
    {
        private readonly IPage _page;
        private readonly ILocator _forenameInput;
        private readonly ILocator _emailInput;
        private readonly ILocator _messageInput;
        private readonly ILocator _submitButton;
        private readonly ILocator _successMessage;
        private readonly ILocator _forenameError;
        private readonly ILocator _emailError;
        private readonly ILocator _messageError;

        public ContactPage(IPage page)
        {
            _page = page;
            _forenameInput = _page.Locator("#forename");
            _emailInput = _page.Locator("#email");
            _messageInput = _page.Locator("#message");
            _submitButton = _page.Locator("a.btn-contact");
            _successMessage = _page.Locator("div.alert:has-text('Thanks')");
            _forenameError = _page.Locator("#forename-err");
            _emailError = _page.Locator("#email-err");
            _messageError = _page.Locator("#message-err");
        }

        public async Task NavigateAsync()
        {
            await _page.GotoAsync("http://jupiter.cloud.planittesting.com/#/contact");
        }

        public async Task FillFormAsync(string forename, string email, string message)
        {
            await _forenameInput.FillAsync(forename);
            await _emailInput.FillAsync(email);
            await _messageInput.FillAsync(message);
        }

        public async Task SubmitFormAsync()
        {
            await _submitButton.ClickAsync();
        }

        public async Task VerifySuccessMessageAsync()
        {
            try
            {
                await _successMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
                await Assertions.Expect(_successMessage).ToContainTextAsync("Thanks", new() { Timeout = 30000 });
            }
            catch (Exception)
            {
                await _page.ScreenshotAsync(new() { Path = $"success-message-failure-{DateTime.Now.Ticks}.png" });
                throw;
            }
        }

        public async Task VerifyErrorMessagesAsync(bool forenameError, bool emailError, bool messageError)
        {
            if (forenameError)
                await Assertions.Expect(_forenameError).ToBeVisibleAsync();
            else
                await Assertions.Expect(_forenameError).ToBeHiddenAsync();

            if (emailError)
                await Assertions.Expect(_emailError).ToBeVisibleAsync();
            else
                await Assertions.Expect(_emailError).ToBeHiddenAsync();

            if (messageError)
                await Assertions.Expect(_messageError).ToBeVisibleAsync();
            else
                await Assertions.Expect(_messageError).ToBeHiddenAsync();
        }

        public async Task ClearFormAsync()
        {
            await _forenameInput.FillAsync("");
            await _emailInput.FillAsync("");
            await _messageInput.FillAsync("");
        }
    }
}