using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using log4net;
using log4net.Config;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using System.Threading;

namespace SamgauTest
{
    class GoogleTesting3
    {
        private static readonly ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        IWebDriver driver;
        string googleurl = "http://www.google.com";

        bool belowTen = true;
        bool calculator = true;
        bool belowTen_Pages = true;

        public void Scenario2Case1to2(IWebElement element) 
        {
            element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[1]/div/div[2]/input"));
            element.SendKeys("Orange");
            element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[3]/center/input[1]"));
            //*[@id="tsf"]/div[2]/div[1]/div[3]/center/input[1]
            element.Click();
            for(int i = 0; i < 11; i++)
            {
                try
                {
                    element = driver.FindElement(By.XPath(@"//*[@id=""rso""]/div[" + i + "]"));
                }
                catch (NoSuchElementException) {
                    if (i <= 9)
                    {
                        this.belowTen = true;
                    }
                    else
                    {
                        this.belowTen = false;
                        break;
                    }
                }

            }



            for(int i = 2; i < 12; i++)
            {
                try
                {
                    element = driver.FindElement(By.XPath(@"//*[@id=""xjs""]/div/table/tbody/tr/td[" + i + "]"));
                }
                catch (NoSuchElementException)
                {
                    if (i == 2)
                    {
                        this.belowTen_Pages = false;
                        break;
                    }
                    else
                    {
                        this.belowTen_Pages = true;
                    }
                }

            }

            if (this.belowTen == true)
                log.Info("Test Case: TS_TS2_1, Result: Pass");
            else
                log.Info("Test Case: TS_TS2_1, Result: NotPass");

            if (this.belowTen_Pages == true)
                log.Info("Test Case: TS_TS2_2, Result: Pass");
            else
                log.Info("Test Case: TS_TS2_2, Result: NotPass");


        }

        public void Scenario2Case2to4(IWebElement element)
        {

            driver.Url = this.googleurl;

            string plus = "+";
            string minus = "-";
            string multi = "*";
            string divis = "/";

            string resultExp = "";

            Random random = new Random();
            int a = random.Next(2000);
            int b = random.Next(2000);
            int op = random.Next(4);

            string mathExpression = "";

            List<string> operaotors = new List<string>() { plus, minus, multi, divis };

            if (op == 0)
            {
                mathExpression = a + plus + b;
                resultExp = (a + b).ToString();
            }
            else
            if (op == 1)
            {
                mathExpression = a + minus + b;
                resultExp = (a - b).ToString();
            }
            else
            if (op == 2)
            {
                mathExpression = a + multi + b;
                resultExp = (a * b).ToString();
            }
            else
            if (op == 3)
            {
                mathExpression = a + divis + b;
                resultExp = (a / b).ToString();
            }
            else
            {
                mathExpression = a + plus + b;
                resultExp = (a + b).ToString();
            }

            element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[1]/div/div[2]/input"));
            element.SendKeys(mathExpression);
            element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[3]/center/input[1]"));
            element.Click();

            element = driver.FindElement(By.XPath(@"//*[@id=""cwos""]"));

            

            if (!element.Text.Equals(resultExp))
            {
                calculator = false;
            }


            if (calculator == true)
                log.Info("Test Case: TS_TS2_3, Result: Pass");
            else
                log.Info("Test Case: TS_TS2_3, Result: NotPass");

        }

            public void goBack()
        {
            driver.Url = googleurl;
        }

        [SetUp]
        public void startBrowser()
        {
            XmlConfigurator.Configure();
            driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application\\");
            //log = LogManager.GetLogger(GetType());
        }

        [Test]
        public void TestSearchResultFunctionality()
        {
            this.goBack();

            log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetAllRepositories().FirstOrDefault();
            FileAppender appender = repository.GetAppenders().OfType<FileAppender>().FirstOrDefault();
            log.DebugFormat("log file located at : {0}", appender.File);
            IWebElement element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[3]/center/input[2]"));

            log.Info("Scenario TS_2:");
            this.Scenario2Case1to2(element);
            this.Scenario2Case2to4(element);

        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
