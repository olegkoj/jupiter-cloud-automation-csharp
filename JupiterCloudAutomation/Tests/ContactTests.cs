using Microsoft.Playwright.MSTest;
using Microsoft.Playwright;
using JupiterCloudAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Tests
{
    [TestClass]
    public class ContactTests : PageTest
    {
        private ContactPage _contactPage = null!;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _contactPage = new ContactPage(Page);
            await _contactPage.NavigateAsync();
        }

        [TestMethod]
        public async Task TestCase1_VerifyErrorMessagesAndClearErrors()
        {
            await _contactPage.SubmitFormAsync();
            await _contactPage.VerifyErrorMessagesAsync(forenameError: true, emailError: true, messageError: true);
            await _contactPage.FillFormAsync("John Doe", "john.doe@example.com", "Test message");
            await _contactPage.VerifyErrorMessagesAsync(forenameError: false, emailError: false, messageError: false);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public async Task TestCase2_SuccessfulSubmission(int run)
        {
            await _contactPage.FillFormAsync($"User{run}", $"user{run}@example.com", $"Message {run}");
            await _contactPage.SubmitFormAsync();
            await _contactPage.VerifySuccessMessageAsync();
            await _contactPage.ClearFormAsync();
        }
    }
}