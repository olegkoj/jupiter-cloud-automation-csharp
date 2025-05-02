using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JupiterCloudAutomation.Pages;
using System;
using System.Threading.Tasks;

namespace JupiterCloudAutomation.Tests
{
    [TestClass]
    public class ContactTests : PageTest
    {
        private HomePage? _homePage;
        private ContactPage? _contactPage;

        [TestInitialize]
        public async Task TestInitialize()
        {
            try
            {
                await Page.GotoAsync("http://jupiter.cloud.planittesting.com");
                _homePage = new HomePage(Page);
                _contactPage = new ContactPage(Page);
                await _homePage.GotoContactPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TestInitialize failed: {ex.Message}");
                throw;
            }
        }

        [TestMethod]
        public async Task TestCase1_VerifyErrorMessagesAndClearErrors()
        {
            try
            {
                await _contactPage!.ClickSubmit();
                await _contactPage.VerifyErrorMessages();
                await _contactPage.PopulateMandatoryFields("John Doe", "john@example.com", "Test message");
                await _contactPage.VerifyErrorsGone();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TestCase1 failed: {ex.Message}");
                throw;
            }
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public async Task TestCase2_SuccessfulSubmission(int run)
        {
            try
            {
                await _contactPage!.PopulateMandatoryFields("John Doe", "john@example.com", "Test message");
                await _contactPage.ClickSubmit();
                await _contactPage.VerifySuccessMessage();
            }
            catch (Exception ex)
            {
                await Page.ScreenshotAsync(new() { Path = $"testcase2-run-{run}-failure.png" });
                Console.WriteLine($"TestCase2 (run {run}) failed: {ex.Message}");
                throw;
            }
        }
    }
}