using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace DropDownManipulations
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [TearDown]

        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void DropDownTest()
        {
            var dropDown = new SelectElement(driver.FindElement(By.Name("manufacturers_id")));

            string path = System.IO.Directory.GetCurrentDirectory() + "/manufacturer.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            IList<IWebElement> allManufacturers = dropDown.Options;

            List<String> manufNames = new List<String>();

            foreach (var manufName in allManufacturers)
            {
                manufNames.Add(manufName.Text);
            }
            manufNames.RemoveAt(0);

            foreach (var mname in manufNames)
            {
                dropDown.SelectByText(mname);
                dropDown = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturers_id']")));

                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {mname} has no products.");
                }

                else
                {
                    var productTable = driver.FindElement(By.ClassName("productListingData"));

                    File.AppendAllText(path, $"\n\nThe manufacturer {mname} products are listes--\n");

                    IReadOnlyCollection<IWebElement> trows = productTable.FindElements(By.XPath(".//tbody/tr"));

                    foreach (var row in trows)
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }
            }
        }
    }
}