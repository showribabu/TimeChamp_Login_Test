using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Timechamp_Login_Testing.Test;
using System.Drawing.Imaging;
using System.IO;
namespace Timechamp_Login_Testing
{
    [TestClass]
    public class UnitTest1
    {

        private readonly Dictionary<string, string> vars = new Dictionary<string, string>();
        private  IWebDriver driver; 
        private LoginPage loginPage;
        private string Url;
        private string refresh_Url;
        private string forgot_password_Url;
        private string forgot_to_signin_Url;

        [TestInitialize]
        public void Setup()
        {

            //Url = "https://snovasys.sites1.timechamp.io/";

            //Url="https://login.timechamp.io/";
            //refresh_Url = "https://login.timechamp.io/sessions/signin";

            //Url = "https://test254.sites3.timechamp.io/";
            //refresh_Url = "https://test254.sites3.timechamp.io/signin";
            //forgot_password_Url = "https://test254.sites3.timechamp.io/sessions/forgot-password";
            //forgot_to_signin_Url = "https://test254.sites3.timechamp.io/sessions/signin";

            Url = "https://btrak272-development.snovasys.com/signin";
            refresh_Url = "https://btrak272-development.snovasys.com/signin";
            forgot_password_Url = "https://btrak272-development.snovasys.com/sessions/forgot-password";
            forgot_to_signin_Url = "https://btrak272-development.snovasys.com/sessions/signin";



            // Initialize variables
            //vars["valid_username"] = "mahidhar@snovasys.com";
            vars["valid_username"] = "btrak272-development@gmail.com";
            vars["valid_password"] = "Test123!";

            vars["invalid_username"] = "testing@gmail.com";
            vars["invalid_password"] = "Test123";

            vars["inactive_username"] = "testing@gmail.com";
            vars["inactive_password"] = "Test123";

            vars["purged_username"] = "testing@gmail.com";
            vars["purged_password"] = "Test123";

            vars["SubscriptionOver_username"] = "testing@gmail.com";
            vars["SubscriptionOver_password"] = "Test123";

            vars["TrialExpiration_username"] = "testing@gmail.com";
            vars["TrialExpiration_password"] = "Test123!";

        }

        // Testcases - 1


        public void TestLoginFromIncognito()
        {
            //To login to another account, open incognito mode and navigate to the Time Champ site
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            IWebDriver incognitoDriver = new ChromeDriver(options);
            incognitoDriver.Navigate().GoToUrl(Url);
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Signin", incognitoDriver.Title);
            Console.WriteLine("Exception: The Page Title is " + incognitoDriver.Title);

            incognitoDriver.Quit();
        }

        public void TestAutoLogin()
        {
            TestValidLogin();
            //1: Check if auto login occurs by default - Navigate to the site and check if the user is already logged in
            driver.Navigate().GoToUrl(Url);
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(driver => driver.FindElements(By.CssSelector(".user-name")).Count > 0);
                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :Auto Login not occur -" + ex.ToString());

            }

        }
        public void TestValidLogin()
        {
            // Enter valid credentials and Click on "Sign in"
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);
            driver.Navigate().GoToUrl(Url);
            loginPage.Login(vars["valid_username"].ToString(), vars["valid_password"].ToString());
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(Timeout.Infinite));
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
                wait.Until(driver => driver.FindElements(By.CssSelector(".user-name")).Count > 0);
                Console.WriteLine("Login Successful");
                //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Activity Dashboard", driver.Title, "Login was not successful");

            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception : Login was not successful - " + ex.ToString());
            }
        }

        //Testcase -2 

        public void TestSignInWithBlankFields()
        {
            try
            {
                driver = new ChromeDriver();

                loginPage = new LoginPage(driver);

                driver.Navigate().GoToUrl(Url);
                // 2. If the user keeps both the Username, Password fields blank and checks sign in
                //loginPage.Login(vars["null_username"],vars["null_password"]);
                driver.FindElement(By.CssSelector(".mat-button-wrapper > span")).Click();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(IsSignInButtonDisabled(), "Sign-in button is not disabled");
            
                driver.Quit();
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public void TestSignInWithBlankUsername()
        {
            try
            {
                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                driver.FindElement(By.Id("username")).Click();
                driver.FindElement(By.Id("password")).SendKeys(vars["valid_password"]);
                driver.FindElement(By.CssSelector(".mat-button-wrapper > span")).Click();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(IsSignInButtonDisabled(), "Sign-in button is not disabled");
                //Assert.IsTrue(IsSignInButtonDisabled(), "Sign-in button is not disabled
                driver.Quit();
            }

            catch (Exception e)
            {

                Console.WriteLine("Exception: " + e.ToString());

            }

        }
        public void TestSignInWithBlankPassword()
        {
            try
            {
                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                // 4. If the user fills Username, Password is blank and checks sign in
                //loginPage.Login(vars["valid_username"], vars["null_password"]);
                driver.FindElement(By.Id("username")).Click();

                driver.FindElement(By.Id("username")).SendKeys(vars["valid_username"].ToString());

                driver.FindElement(By.Id("password")).Click();
                driver.FindElement(By.CssSelector(".mat-button-wrapper > span")).Click();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(IsSignInButtonDisabled(), "Sign-in button is not disabled");
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public void TestSignInWithInvalidCredentials()
        {
            try
            {
                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                // 5. If the user fills Username, Password with invalid details and checks sign in
                loginPage.Login(vars["invalid_username"], vars["invalid_password"]);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(ErrorMessge_check(), "Invalid credentials message not found");
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.ToString());
            }
        }



        public void TestSignInAfterTrialExpiration()
        {
            try
            {

                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                // 6. If the trial expires, fills Username, Password with valid details and checks sign in
                loginPage.Login(vars["TrialExpiration_username"].ToString(), vars["TrialExpiration_password"].ToString());
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(ErrorMessge_check(), "Trial expiration message not found");

                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public void TestSignInAfterSubscriptionOver()
        {
            try
            {

                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                // 7. If the subscription is over, fills Username, Password with valid details and checks sign in
                loginPage.Login(vars["SubscriptionOver_username"].ToString(), vars["SubscriptionOver_password"].ToString());
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(ErrorMessge_check(), "Subscription over message not found");
                //Assert.IsFalse(ErrorMessge_check(), "Subscription over message not found");
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e);
            }
        }

        public void TestSignInWithInactiveAccount()
        {
            try
            {

                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                // 8. If the account is inactive, fills Username, Password with valid details and checks sign in
                loginPage.Login(vars["inactive_username"].ToString(), vars["inactive_password"].ToString());
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(ErrorMessge_check(), "Inactive account message not found");
                //Assert.IsFalse(ErrorMessge_check(), "Inactive account message not found");
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e);
            }
        }

        public void TestSignInWithPurgedAccount()
        {
            try
            {
                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                driver.Navigate().GoToUrl(Url);
                // 9. If the account is purged, fills Username, Password with valid details and checks sign in
                loginPage.Login(vars["purged_username"].ToString(), vars["purged_password"].ToString());
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(ErrorMessge_check(), "Purged account message not found");
                //Assert.IsFalse(ErrorMessge_check(), "Purged account message not found");
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception :" + e.ToString());
            }
        }


        private bool IsSignInButtonDisabled()
        {

            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));
            IWebElement msg = wait.Until(driver => driver.FindElement(By.CssSelector(".form-error-msg")));
            return msg.Displayed;

        }


        private bool ErrorMessge_check()
        {
            try
            {
                // Wait for the password expired message to be present
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Check if the element with *ngIf="PasswordExpired" is visible
                IWebElement errormsg = wait.Until(driver => driver.FindElement(By.CssSelector(".mat-error-sign-in .error-message")));

                // Check if the element is displayed
                return errormsg.Displayed;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                return false;
            }
        }


        //Testcase-3
        public void VersionCheck()
        {
            driver = new ChromeDriver();
            // Navigate to the login page
            driver.Navigate().GoToUrl("https://login.timechamp.io/");
            IWebElement logoElement = driver.FindElement(By.CssSelector(".logo_btrak"));
            // Create an Actions object for performing mouse-hover actions on logo
            Actions actions = new Actions(driver);
            actions.MoveToElement(logoElement).Perform();
            // Wait for a short period (adjust as needed) for the version tooltip to appear
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            string version = logoElement.GetAttribute("title");
            try
            {
                // Assert the version is not null or empty
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(string.IsNullOrEmpty(version), "Version information not found.");
                Console.WriteLine($"Time Champ Version: {version}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : No version information found - " + e.ToString());
            }

        }
        //Testcase -4
        public void LogoNavigation()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://login.timechamp.io/");

            // Locate the Time Champ logo element
            IWebElement logoElement = driver.FindElement(By.CssSelector(".logo_btrak"));
            string currentUrlBeforeClick = driver.Url;
            // Click on the logo
            logoElement.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            // Get the current URL after clicking on the logo
            string currentUrlAfterClick = driver.Url;
            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("https://www.snovasys.com/", currentUrlAfterClick);
            }
            catch (Exception ex)
            {
                // Output the navigation URLs (optional)
                Console.WriteLine("Exception : Logo navigation is incorrect. - " + ex.ToString());
                Console.WriteLine($"Before Click: {currentUrlBeforeClick}");
                Console.WriteLine($"After Click: {currentUrlAfterClick}");
            }


        }

        //testcase -5
        public void RefreshPage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Url);
            // Perform a full page refresh using Keys class and SendKeys method
            driver.FindElement(By.TagName("body")).SendKeys(Keys.Control + Keys.F5);
            // driver.FindElement(By.TagName("body")).SendKeys(Keys.F5);
            System.Threading.Thread.Sleep(2000);
            // Get the current URL after the page refresh
            string UrlAfterRefresh = driver.Url;

            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(refresh_Url, UrlAfterRefresh);
                Console.WriteLine($"URL after refresh: {UrlAfterRefresh}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : Page refresh did not result in the same page. -" + e.ToString());
                // Output the current URL after refresh (optional)
                Console.WriteLine($"URL after refresh: {UrlAfterRefresh}");
            }

        }

        //Testcase -6
        public void ForgotPasswordNavigation()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Url);
            //IWebElement forgotPasswordLink = driver.FindElement(By.PartialLinkText("Forgot Password"));
            IWebElement forgotPasswordLink = driver.FindElement(By.LinkText("Forgot Password?"));

            // Get the current URL before clicking on the "Forgot password" link
            string currentUrlBeforeClick = driver.Url;
            forgotPasswordLink.Click();
            System.Threading.Thread.Sleep(2000);
            // Get the current URL after clicking on the "Forgot password" link
            string currentUrlAfterClick = driver.Url;
            try
            {
                // Assert that the navigation URL is as expected (replace with the actual URL)
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(forgot_password_Url, currentUrlAfterClick);
                System.Console.WriteLine($" URL After Click: {currentUrlAfterClick}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : Forgot password navigation is incorrect. -" + ex.ToString());
                System.Console.WriteLine($"Before Click: {currentUrlBeforeClick}");
                System.Console.WriteLine($"After Click: {currentUrlAfterClick}");
            }

        }

        //Testcase-7
        public void ForgotPasswordVersionCheck()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://login.timechamp.io/sessions/forgot-password");
            IWebElement logoElement = driver.FindElement(By.CssSelector(".logo_btrak"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(logoElement).Perform();

            string version = logoElement.GetAttribute("title");
            try
            {
                // Assert the version is not null or empty
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(string.IsNullOrEmpty(version), "Version information not found.");
                Console.WriteLine($"Time Champ Version on Forgot password page: {version}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: No version information found in Forgot passsword Page - " + e.ToString());
            }
        }

        //Testcase-8
        public void ForgotPasswordRefresh()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(forgot_password_Url);
            driver.FindElement(By.TagName("body")).SendKeys(Keys.Control + Keys.F5);
            System.Threading.Thread.Sleep(2000);
            string UrlAfterRefresh = driver.Url;

            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(forgot_password_Url, UrlAfterRefresh);
                Console.WriteLine($"URL after refresh in Forgot password page: {UrlAfterRefresh}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: Page refresh did not result in the same page. -" + e.ToString());
                Console.WriteLine($"URL after refresh in forgot password page : {UrlAfterRefresh}");
            }

        }

        //Testcase -9
        public void SigninNavigation()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(forgot_password_Url);
            IWebElement signinLink = driver.FindElement(By.CssSelector(".mat-accent > .mat-button-wrapper"));
            string currentUrlBeforeClick = driver.Url;
            signinLink.Click();
            System.Threading.Thread.Sleep(2000);
            // Get the current URL after clicking on the "Sign in" link
            string currentUrlAfterClick = driver.Url;
            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(forgot_to_signin_Url, currentUrlAfterClick);
                System.Console.WriteLine($" URL After Click: {currentUrlAfterClick}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: Signin navigation is incorrect. -" + ex.ToString());
                System.Console.WriteLine($"Before Click: {currentUrlBeforeClick}");
                System.Console.WriteLine($"After Click: {currentUrlAfterClick}");
            }
        }

        //Testcase -10
        public void LogoutFunctionality()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);
            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            Console.WriteLine(profile.Text);
            profile.Click();

            // Locate and click on the "Sign Out" option
            IWebElement signOutButton = driver.FindElement(By.XPath("//button[contains(., 'Sign Out')]")); // Adjust the XPath based on your application
            signOutButton.Click();

            string currenturl = driver.Url;
            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(forgot_to_signin_Url, currenturl);
                Console.WriteLine("Signed out Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :Not Signed out" + ex.ToString());
            }

        }

        //Testcase -11
        public void LoginPerformance()
        {

            // Start measuring the time for login and stop after valid login
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // Perform the login
            //loginPage.Login(vars["valid_username"].ToString(), vars["valid_password"].ToString());
            TestValidLogin();
            stopwatch.Stop();
            // Assert that the login was performed within the expected time (adjust the threshold as needed)
            Console.WriteLine("The time taken for the valid Login :" + stopwatch.Elapsed);
            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(stopwatch.Elapsed.TotalSeconds < 60);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :The total time taken in seconds:" + stopwatch.Elapsed.TotalSeconds);
                Console.WriteLine(ex.ToString());
            }

        }

        //Testcase -12

        public void VerifyLoginPageUi()
        {

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Url);

            // Check if the username input field and password field  and submit button and forgot password link is presenttry

            try
            {
                IWebElement usernameInput = driver.FindElement(By.Id("username"));
                IWebElement userpasswordInput = driver.FindElement(By.Id("password"));
                IWebElement loginButton = driver.FindElement(By.CssSelector(".mat-button-wrapper > span"));
                IWebElement forgotPasswordLink = driver.FindElement(By.LinkText("Forgot Password?"));

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(usernameInput.Displayed, "Username input field is not displayed on the login page.");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(userpasswordInput.Displayed, "userpassword input field is not displayed on the login page.");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(loginButton.Displayed, "Login button is not displayed on the login page.");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(forgotPasswordLink.Displayed, "Forgot Password link is not displayed on the login page.");

                Console.WriteLine("The UI contain the required fields.");
            }
            catch (Exception e)
            {
                Console.WriteLine(" Exception :The UI not contain the required fields." + e.ToString());
            }
        }

        //Testcase -13
        public void LoginAudit()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            // Locate and click on the profile element
            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            // Check if the "Login Audit" menu is present
            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                // Click on the "Login Audit" menu
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                // Check if the login audit table headers and record count are greater than one
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    wait.Until(driver => driver.FindElements(By.XPath("//span[contains(.,'Date Time')]")).Count > 0);
                    Console.WriteLine("The Audit Login View sucessful");
                    IWebElement firstRecord = driver.FindElement(By.CssSelector(".k-grid-content table tr:nth-child(2)")); // Adjust the selector based on your HTML structure

                    // Print the extracted information
                    Console.WriteLine("First Record -" + firstRecord.Text);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception :  The DateTime element in the Login audit  not present" + e.ToString());
                }

            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        //Testcase -14
        public void VerifySignoutStatusInLoginAudit()
        {

            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);
            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {

                // Perform logout from the profile
                IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
                profile.Click();

                // Check if the "Logout" menu is present
                if (IsElementPresent(driver, By.XPath("//button[contains(., 'Logout')]")))
                {

                    IWebElement logoutMenu = driver.FindElement(By.XPath("//button[contains(., 'Logout')]"));
                    logoutMenu.Click();
                    string currenturl = driver.Url;
                    try
                    {
                        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(forgot_to_signin_Url, currenturl);
                        Console.WriteLine("Signed out Successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception :Not Signed out" + ex.ToString());
                    }

                }
                else
                {
                    Console.WriteLine("Logout menu is not present.");
                }
            }

            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        //Testcase -15
        public void LoginAuditUITest()
        {

            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);
            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();
            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {

                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    wait.Until(driver => driver.FindElement(By.XPath("//span[contains(., 'Date Time')]")).Displayed);
                    wait.Until(driver => driver.FindElement(By.XPath("//th[contains(., 'Email')]")).Displayed);
                    Console.WriteLine("Login Audit Contains required fields");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception : not contain required fields - " + e.ToString());
                }

            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }


        //Testcase -16
        public void Export_Excel_LoginAuditTest()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    // find Download button
                    IWebElement downloadButton = driver.FindElement(By.CssSelector(".ml-02 .svg-inline--fa"));
                    Actions builder = new Actions(driver);
                    builder.MoveToElement(downloadButton).Perform();
                    // Click the download button
                    downloadButton.Click();
                    // Wait for the Export Login Audit Details popup to be visible
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    wait.Until(driver => driver.FindElement(By.CssSelector(".export-audit-container")).Displayed);
                    // Export popup for Login Audit 

                    try
                    {
                        driver.FindElement(By.Id("mat-input-3")).Click();
                        //id=mat-input-3 | Optional Title
                        driver.FindElement(By.Id("mat-input-3")).SendKeys("Optional Title");

                        //.xlsx file
                        driver.FindElement(By.CssSelector("#mat-radio-2 .mat-radio-container")).Click();

                        /*
                         // Download / Export button
                        //Click on the Export for the Download ->  css=.cdk-focused > .mat-button-wrapper
                        driver.FindElement(By.CssSelector(".mr-05 > .mat-button-wrapper")).Click();
                        */

                        // Click on the "Cancel" button
                        IWebElement cancelButton = driver.FindElement(By.XPath("//span[contains(.,' Cancel')]"));
                        cancelButton.Click();
                        Console.WriteLine("Login Audit Details (optional  to Download )Cancel success fully!!");
                        Console.WriteLine("Login Audit Details Downloaded success fully!!(But not  commented the downoload option)");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error While Export Audit Login popup" + e.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while clicking the download button: " + e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        //Testcase -17
        public void Export_CSV_LoginAuditTest()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    // find Download button
                    IWebElement downloadButton = driver.FindElement(By.CssSelector(".ml-02 .svg-inline--fa"));
                    Actions builder = new Actions(driver);
                    builder.MoveToElement(downloadButton).Perform();
                    // Click the download button
                    downloadButton.Click();
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    wait.Until(driver => driver.FindElement(By.CssSelector(".export-audit-container")).Displayed);
                    try
                    {
                        driver.FindElement(By.Id("mat-input-3")).Click();
                        driver.FindElement(By.Id("mat-input-3")).SendKeys("Optional Title");
                        // .csv file
                        driver.FindElement(By.CssSelector("#mat-radio-3 .mat-radio-outer-circle")).Click();

                        /*
                         // Download / Export button
                        //Click on the Export for the Download ->  css=.cdk-focused > .mat-button-wrapper
                        driver.FindElement(By.CssSelector(".mr-05 > .mat-button-wrapper")).Click();
                        */

                        // Click on the "Cancel" button
                        IWebElement cancelButton = driver.FindElement(By.XPath("//span[contains(.,' Cancel')]"));
                        cancelButton.Click();
                        Console.WriteLine("Login Audit Details in CSV format  canceled success fully!!");
                        Console.WriteLine("Login Audit Details Downloaded success fully!! (optional - > only for testing working or not)!!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error while clicking the download button: " + e.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while clicking the download button: " + e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        //Testcase-18
        public void VerifyLoginAuditPagination()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    // Example using XPath
                    IWebElement tableBody = driver.FindElement(By.XPath("//tbody[@kendogridtablebody]"));
                    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(20));
                    IList<IWebElement> rows = tableBody.FindElements(By.TagName("tr"));
                    // Get the count of rows
                    int rowCount = rows.Count;
                    // Output the count of rows - here the content dynamically loaded ,here it needs ot contain atleast 1 record
                    //Console.WriteLine("Number of rows in the table: " + rowCount);
                    Console.WriteLine("Pagination or Data recors avilable in the login Audit");
                }
                catch (Exception e)
                {
                    Console.WriteLine("No Table Avilabe in the Pagination " + e.ToString());
                }


            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }


        }
        //Testcase -19
        public void LoginAuditHistory()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    wait.Until(driver => driver.FindElement(By.XPath("//span[contains(., 'Date Time')]")).Displayed);

                    // Add assertions for each column in the Login Audit table

                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(.,'Email ')]")).Displayed, "Email column not found");
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(.,'Status')]")).Displayed, "Status column not found");
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(.,'City Name')]")).Displayed, "City Name column not found");
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(.,'IP address')]")).Displayed, "IP Address column not found");
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(.,'Browser')]")).Displayed, "Browser column not found");

                    Console.WriteLine("Login Audit Contains required fields");


                    IWebElement tableBody = driver.FindElement(By.CssSelector(".k-grid-content tbody"));

                    // Get all rows in the table
                    IList<IWebElement> rows = tableBody.FindElements(By.TagName("tr"));
                    if (rows.Count == 0)
                    {
                        Console.WriteLine("No records found in the Login Audit table.");

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("NO such elements found" + e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        //Testccase 20 
        public void LoginAuditSearch()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    IWebElement searchInput = driver.FindElement(By.Id("mat-input-2"));
                    searchInput.Click();
                    searchInput.SendKeys("@gmail.com");
                    searchInput.SendKeys(Keys.Enter);
                    Console.WriteLine("Search ELement Identified");
                }
                catch (Exception e)
                {
                    Console.WriteLine("search option not visible" + e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }
        //Testcase 21
        public void LoginAuditAtProfile()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();
                Console.WriteLine("Login Audit Avilable in the Profile!!!");

            }
            else
            {
                Console.WriteLine("Login Audit  is not present.");
            }
        }
        //fa-times
        //Testcase 22
        public void LoginAuditPopupClose()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    IWebElement closeicon = driver.FindElement(By.CssSelector(".fa-times"));
                    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                    closeicon.Click();
                    Console.WriteLine("Login Audit Popup Closed");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while find the element - " + e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        // .k-header:nth-child(1) .k-icon
        //Testcase 23
        public void HideAndViewFunctionality()
        {
            TestValidLogin();
            driver.Manage().Window.Size = new System.Drawing.Size(1542, 812);

            IWebElement profile = driver.FindElement(By.CssSelector(".user-name"));
            profile.Click();

            if (IsElementPresent(driver, By.XPath("//button[contains(., 'Login Audit')]")))
            {
                IWebElement loginAuditMenu = driver.FindElement(By.XPath("//button[contains(., 'Login Audit')]"));
                loginAuditMenu.Click();

                try
                {
                    {
                        var element = driver.FindElement(By.CssSelector(".k-header:nth-child(1) > .k-link"));
                        Actions builder = new Actions(driver);
                        builder.MoveToElement(element).Perform();
                    }
                    Console.WriteLine("Mouse hover to the Hiding element get the three dot menu!! ");
                    driver.FindElement(By.CssSelector(".k-header:nth-child(1) .k-icon")).Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    //checkboxes..
                    //first check box
                    driver.FindElement(By.CssSelector(".k-column-list-item:nth-child(1) > .k-checkbox")).Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                    /*
                    // reset
                    driver.FindElement(By.CssSelector(".k-button:nth-child(1)")).Click();
                  */
                    // apply
                    driver.FindElement(By.CssSelector(".k-primary")).Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                    Console.WriteLine("Applied the unselection of check box successfully!! ");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Login Audit menu is not present.");
            }
        }

        
        [TestMethod]
        [Category ("Testcase1 for Valid login and Auto Login and Incognito window")]
        public void Testcase01()
        {

            TestValidLogin();
            TestAutoLogin();         
            TestLoginFromIncognito();

        }

        [TestMethod]
        public void Testcase02()
        {

            TestLoginFromIncognito();
            TestSignInWithBlankFields();
            TestSignInWithBlankUsername();
            TestSignInWithBlankPassword();
            TestSignInWithInvalidCredentials();
            TestSignInAfterTrialExpiration();
            TestSignInAfterSubscriptionOver();
            TestSignInWithInactiveAccount();
            TestSignInWithPurgedAccount();

        }

        [TestMethod]
        public void Testcase03()
        {
            VersionCheck();
        }

        [TestMethod]
        public void Testcase04()
        {
            LogoNavigation();
        }

        [TestMethod]
        public void Testcase05()
        {
            RefreshPage();
        }

        [TestMethod]
        public void Testcase06()
        {
            ForgotPasswordNavigation();
        }

        [TestMethod]
        public void Testcase07()
        {
            ForgotPasswordVersionCheck();
        }

        [TestMethod]
        public void Testcase08()
        {
            ForgotPasswordRefresh();

        }

        [TestMethod]
        public void Testcase09()
        {
            SigninNavigation();
        }

        [TestMethod]
        public void Testcase10()
        {
            LogoutFunctionality();
        }

        [TestMethod]
        public void Testcase11()
        {
            LoginPerformance();
        }

        [TestMethod]
        public void Testcase12()
        {
            VerifyLoginPageUi();
        }

        [TestMethod]
        public void Testcase13()
        {
            LoginAudit();
        }

        // need to test

        [TestMethod]
        public void Testcase14()
        {
            VerifySignoutStatusInLoginAudit();
        }
        [TestMethod]
        public void Testcase15()
        {
            LoginAuditUITest();

        }
        [TestMethod]
        public void Testcase16()
        {
            Export_Excel_LoginAuditTest();
        }

        [TestMethod]
        public void Testcase17()
        {
            Export_CSV_LoginAuditTest();
        }

        [TestMethod]
        public void Testcase18()
        {
            VerifyLoginAuditPagination();
        }
        [TestMethod]
        public void Testcase19()
        {
            LoginAuditHistory();
        }

        [TestMethod]
        public void Testcase20()
        {
            LoginAuditSearch();
        }
        [TestMethod]
        public void Testcase21()
        {
            LoginAuditAtProfile();
        }
        [TestMethod]
        public void Testcase22()
        {
            LoginAuditPopupClose();
        }

        [TestMethod]
        public void Testcase23()
        {
            HideAndViewFunctionality();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CaptureScreenshot();
            // This is called after each test method
            driver.Quit();
        }

        private void CaptureScreenshot()
        {
            try
            {
                // Capture screenshot
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                // Create the Screenshots folder path
                var screenshotsFolderPath = Path.Combine(Environment.CurrentDirectory, @"..\..\Screenshots\");

                // Check if the directory exists, if not, create it
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                // Create a unique filename based on the test method name
                var screenshotFileName = $"{NUnit.Framework.TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMddHHmmssfff}.png";

                // Combine the path to the Screenshots folder with the filename
                var screenshotPath = System.IO.Path.Combine(screenshotsFolderPath, screenshotFileName);

                /*
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath));
                */

                using (var stream = new FileStream(screenshotPath, FileMode.Create))
                {
                    stream.Write(screenshot.AsByteArray, 0, screenshot.AsByteArray.Length);
                }



                // Output the path to the console (optional)
                Console.WriteLine($"Screenshot saved at: {screenshotPath}");
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log or throw
                Console.WriteLine($"Error capturing screenshot: {ex.Message}");
            }
        }

    }
}
