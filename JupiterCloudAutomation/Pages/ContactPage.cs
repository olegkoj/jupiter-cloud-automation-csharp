using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Pages
{
    public class ContactPage
    {
        private readonly IPage _page;
        private readonly ILocator _submitButton;
        private readonly ILocator _forenameInput;
        private readonly ILocator _emailInput;
        private readonly ILocator _messageInput;
        private readonly ILocator _forenameError;
        private readonly ILocator _emailError;
        private readonly ILocator _messageError;
        private readonly ILocator _successMessage;

        public ContactPage(IPage page)
        {
            _page = page;
            _submitButton = page.Locator("a.btn-contact");
            _forenameInput = page.Locator("#forename");
            _emailInput = page.Locator("#email");
            _messageInput = page.Locator("#message");
            _forenameError = page.Locator("#forename-err");
            _emailError = page.Locator("#email-err");
            _messageError = page.Locator("#message-err");
            _successMessage = page.Locator("div.alert.alert-success");
        }

        public async Task ClickSubmit()
        {
            await _submitButton.ClickAsync();
        }

        public async Task VerifyErrorMessages()
        {
            await Assertions.Expect(_forenameError).ToHaveTextAsync("Forename is required");
            await Assertions.Expect(_emailError).ToHaveTextAsync("Email is required");
            await Assertions.Expect(_messageError).ToHaveTextAsync("Message is required");
        }

        public async Task PopulateMandatoryFields(string forename, string email, string message)
        {
            await _forenameInput.FillAsync(forename);
            await _emailInput.FillAsync(email);
            await _messageInput.FillAsync(message);
        }

        public async Task VerifyErrorsGone()
        {
            await Assertions.Expect(_forenameError).Not.ToBeVisibleAsync();
            await Assertions.Expect(_emailError).Not.ToBeVisibleAsync();
            await Assertions.Expect(_messageError).Not.ToBeVisibleAsync();
        }

        public async Task VerifySuccessMessage()
        {
            try
            {
                await _successMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await Assertions.Expect(_successMessage).ToContainTextAsync("Thanks");
            }
            catch (Exception ex)
            {
                await _page.ScreenshotAsync(new() { Path = $"success-message-failure-{DateTime.Now.Ticks}.png" });
                throw;
            }
        }
    }
}