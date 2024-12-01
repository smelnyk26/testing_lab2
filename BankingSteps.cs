using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

[Binding]
public class BankingSteps
{
    private IWebDriver driver;
    private LoginPage loginPage;
    private string initialBalance;

    [Given(@"the banking page is open")]
    public void GivenTheBankingPageIsOpen()
    {
        driver = new ChromeDriver();
        loginPage = new LoginPage(driver);
        loginPage.OpenLoginPage("https://www.globalsqa.com/angularJs-protractor/BankingProject");
    }

    [Given(@"the user logs in as ""(.*)""")]
    public void GivenTheUserLogsInAs(string customerName)
    {
        loginPage.LoginAsCustomer(customerName);
    }

    [Given(@"the user selects account ""(.*)""")]
    public void GivenTheUserSelectsAccount(string accountNumber)
    {
        loginPage.SelectAccount(accountNumber);
        initialBalance = loginPage.GetBalance();
    }

    [When(@"the user deposits ""(.*)""")]
    public void WhenTheUserDeposits(string amount)
    {
        loginPage.PerformDeposit(amount);
    }

    [Then(@"the balance should be updated correctly")]
    public void ThenTheBalanceShouldBeUpdatedCorrectly()
    {
        string finalBalance = loginPage.GetBalance();
        int expectedBalance = int.Parse(initialBalance) + int.Parse("500");
        Assert.That(finalBalance, Is.EqualTo(expectedBalance.ToString()), "The balance is not updated correctly after deposit.");
    }

    [AfterScenario]
    public void CloseBrowser()
    {
        loginPage?.CloseDriver();
    }
}
