using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWebTables
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

        public void Teardown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void WebTableTest()
        {
            IWebElement productTable = driver.FindElement(By.TagName("table"));
            ReadOnlyCollection<IWebElement> tableRows = productTable.FindElements(By.XPath("//tbody//tr"));

            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (var trow in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableCols = trow.FindElements(By.XPath("td"));
                foreach (var tcol in tableCols)
                {
                    String data = tcol.Text;
                    String[] productInfo = data.Split('\n');
                    String printProductInfo = productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n";

                    File.AppendAllText(path, printProductInfo);

                    Assert.IsTrue(File.Exists(path), "CSV file was not created");
                    Assert.IsTrue(new FileInfo(path).Length > 0, "CSV file is empty");
                }
            }
        }
    }
}