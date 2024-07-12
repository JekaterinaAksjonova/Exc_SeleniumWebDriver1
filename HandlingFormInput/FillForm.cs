using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInput
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void FillForm()
        {
            driver.FindElement(By.LinkText("My Account")).Click();
            driver.FindElement(By.LinkText("Continue")).Click();

            driver.FindElement(By.XPath("//input[@value='f']")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Ekaterina");
            driver.FindElement(By.Name("lastname")).SendKeys("Aksjonova");
            driver.FindElement(By.Name("dob")).SendKeys("04/01/1987");

            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            string email = "test" + num.ToString() + "@test.com";
            driver.FindElement(By.Name("email_address")).SendKeys(email);
            driver.FindElement(By.Name("company")).SendKeys("Random company");
            driver.FindElement(By.Name("street_address")).SendKeys("Malgara");
            driver.FindElement(By.Name("street_address")).SendKeys("Malgara");
            driver.FindElement(By.Name("postcode")).SendKeys("4013");
            driver.FindElement(By.Name("city")).SendKeys("Plovdiv");
            driver.FindElement(By.Name("state")).SendKeys("Plovdiv");
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            driver.FindElement(By.Name("telephone")).SendKeys("08999999999");            
            driver.FindElement(By.Name("newsletter")).Click();
            driver.FindElement(By.Name("password")).SendKeys("123456");
            driver.FindElement(By.Name("confirmation")).SendKeys("123456");

            driver.FindElement(By.Id("tdb4")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Your Account Has Been Created!"), "Account creation failed.");

            driver.FindElement(By.LinkText("Log Off")).Click();
            driver.FindElement(By.LinkText("Continue")).Click();
            Console.WriteLine($"Your account created with email: {email}");

        }
    }
}