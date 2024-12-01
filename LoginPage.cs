using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    }

    // Метод для відкриття сторінки
    public void OpenLoginPage(string url)
    {
        driver.Navigate().GoToUrl(url);
    }

    // Метод для входу як Customer
    public void LoginAsCustomer(string customerName)
    {
        IWebElement customerLoginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Customer Login']")));
        customerLoginButton.Click();

        // Вибір користувача зі списку
        SelectElement customerDropdown = new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("userSelect"))));
        customerDropdown.SelectByText(customerName);

        IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Login']")));
        loginButton.Click();
    }

    // Метод для вибору рахунку
    public void SelectAccount(string accountNumber)
    {
        SelectElement accountDropdown = new SelectElement(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("accountSelect"))));
        accountDropdown.SelectByText(accountNumber);
    }

    // Метод для виконання операції Deposit
    public void PerformDeposit(string amount)
    {
        // Натискаємо кнопку "Deposit" для початку внесення коштів
        IWebElement depositButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Deposit')]")));
        depositButton.Click();

        // Вводимо суму депозиту
        IWebElement amountInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@ng-model='amount']")));
        amountInput.Clear();
        amountInput.SendKeys(amount);

        // Підтверджуємо внесення депозиту
        IWebElement submitDepositButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Deposit']")));
        submitDepositButton.Click();
    }

    // Метод для перевірки балансу
    public string GetBalance()
    {
        IWebElement balanceLabel = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//strong[2]")));
        return balanceLabel.Text;
    }

    // Метод для закриття браузера
    public void CloseDriver()
    {
        if (driver != null)
        {
            driver.Quit();
        }
    }
}
