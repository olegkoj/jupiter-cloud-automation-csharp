# Jupiter Cloud Automation - C# Selenium

This repository contains the automation solution for the Planit Technical Assessment using C# and Selenium WebDriver.

## Prerequisites
- .NET SDK 6.0 or higher
- Chrome Browser and ChromeDriver
- Visual Studio or VS Code

## Setup
1. Clone the repository: `git clone https://github.com/olegkoj/jupiter-cloud-automation-csharp.git`
2. Restore dependencies: `dotnet restore`
3. Run tests: `dotnet test`

## Test Cases
- **Test Case 1**: Validates error messages on the contact page when submitting without mandatory fields and ensures errors disappear after filling them.
- **Test Case 2**: Verifies successful submission on the contact page (run 5 times for 100% pass rate).
- **Test Case 3**: Purchases items, verifies subtotals, prices, and total in the cart.

## CI/CD
Configured for Jenkins with a pipeline to run tests and generate reports.

## Notes
- Uses Page Object Model for maintainability.
- Tests are designed to be robust with explicit waits.
- Future improvements: Add parallel test execution and cross-browser testing.
