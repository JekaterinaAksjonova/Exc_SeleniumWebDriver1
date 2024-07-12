using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestCalculator
{
    public class Tests
    {
        IWebDriver driver;
        IWebElement num1;
        IWebElement num2;
        IWebElement operationDropDown;
        IWebElement calcBtn;
        IWebElement resetBtn;
        IWebElement result;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
            num1 = driver.FindElement(By.Id("number1"));
            num2 = driver.FindElement(By.Id("number2"));
            num2 = driver.FindElement(By.Id("number2"));
            operationDropDown = driver.FindElement(By.Id("operation"));
            calcBtn = driver.FindElement(By.Id("calcButton"));
            resetBtn = driver.FindElement(By.Id("resetButton"));
            result = driver.FindElement(By.Id("result"));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public void Calculation(string firstNum, string oper, string secondNum, string expectedRes)
        {
            resetBtn.Click();

            if (!string.IsNullOrEmpty(firstNum))
            {
                num1.SendKeys(firstNum);
            }

            if (!string.IsNullOrEmpty(secondNum))
            {
                num2.SendKeys(secondNum);
            }

            if (!string.IsNullOrEmpty(oper))
            {
                new SelectElement(operationDropDown).SelectByText(oper);
            }
            calcBtn.Click();

            Assert.AreEqual(expectedRes, result.Text);
        }

        [Test]
        [TestCase("5", "+ (sum)", "10", "Result: 15")]
        [TestCase("2e2", "* (multiply)", "1.5", "Result: 300")]
        [TestCase("5", "/ (divide)", "0", "Result: Infinity")]
        [TestCase("invalid", "+ (sum)", "10", "Result: invalid input")]
        public void CalculatorTests(string firstNum, string oper, string secondNum, string expectedRes)
        {
            Calculation(firstNum, oper, secondNum, expectedRes);
        }
    }
}