using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timechamp_Login_Testing.Test
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Page elements
        public IWebElement UsernameInput => driver.FindElement(By.Id("username"));
        public IWebElement PasswordInput => driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => driver.FindElement(By.CssSelector(".mat-button-wrapper > span"));

        // Page methods
        public void Login(string username, string password)
        {
            UsernameInput.Click();
            UsernameInput.SendKeys(username);
            PasswordInput.Click();
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }
    }
}
