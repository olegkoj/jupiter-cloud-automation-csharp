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
            _forenameInput = _page.Locator("input#forename");
            _emailInput = _page.Locator("input#email");
            _messageInput = _page.Locator("textarea#message");
            _submitButton = _page.Locator("a.btn-contact");
            _successMessage = _page.Locator("div.alert:has-text('Thanks')");
            _forenameError = _page.Locator("#forename-err");
            _emailError = _page.Locator("#email-err");
            _messageError = _page.Locator("#message-err");
        }

        public async Task NavigateAsync()
        {
            await _page.GotoAsync("http://jupiter.cloud.planittesting.com/#/contact");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new() { Timeout = 30000 });
            await _page.WaitForSelectorAsync("input#forename", new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
        }

        public async Task FillFormAsync(string forename, string email, string message)
        {
            await _forenameInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await _forenameInput.FillAsync(forename);
            await _emailInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await _emailInput.FillAsync(email);
            await _messageInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await _messageInput.FillAsync(message);
        }

        public async Task SubmitFormAsync()
        {
            await _submitButton.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
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
                await Assertions.Expect(_forenameError).ToBeVisibleAsync(new() { Timeout = 30000 });
            else
                await Assertions.Expect(_forenameError).ToBeHiddenAsync(new() { Timeout = 30000 });

            if (emailError)
                await Assertions.Expect(_emailError).ToBeVisibleAsync(new() { Timeout = 30000 });
            else
                await Assertions.Expect(_emailError).ToBeHiddenAsync(new() { Timeout = 30000 });

            if (messageError)
                await Assertions.Expect(_messageError).ToBeVisibleAsync(new() { Timeout = 30000 });
            else
                await Assertions.Expect(_messageError).ToBeHiddenAsync(new() { Timeout = 30000 });
        }

        public async Task ClearFormAsync()
        {
            try
            {
                await NavigateAsync();
                await _forenameInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
                await _forenameInput.FillAsync("");
                await _emailInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
                await _emailInput.FillAsync("");
                await _messageInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
                await _messageInput.FillAsync("");
            }
            catch (Exception)
            {
                await _page.ScreenshotAsync(new() { Path = $"clear-form-failure-{DateTime.Now.Ticks}.png" });
                throw;
            }
        }
    }
}