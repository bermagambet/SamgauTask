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

namespace SamgauTest
{
    class GoogleTesting1
    {
        private static readonly ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool scenario3case2 = true;

        IWebDriver driver;
        string googleurl = "http://www.google.com";


        public void Scenario3Case1(IWebElement element)
        {
            element.Click();
            string isDoodles = driver.Url;


            if (isDoodles.Equals("https://www.google.com/doodles/"))
            {
                log.Info("Test Case: TS_TS3_1, Result: Pass");
            }
            else if (!isDoodles.Equals("https://www.google.com/doodles/"))
            {
                log.Info("Test Case: TS_TS3_1, Result: NotPass");
            }
        }

        public void Scenario3Case2(IWebElement element)
        {
            element = driver.FindElement(By.XPath(@"//*[@id=""fsl""]/a[1]"));
            element.Click();
            string isAdv = driver.Url;


            if (!isAdv.Equals(@"https://ads.google.com/intl/kk_kz/home/?subid=ww-ww-et-g-awa-a-g_hpafoot1_1!o2&utm_source=google.com&utm_medium=referral&utm_campaign=google_hpafooter&fg=1"))
            {

                //log.Info("Our URL: " + isAdv);
                //log.Info("ReaL URL: " + "https://ads.google.com/intl/en_kz/home/?subid=ww-ww-et-g-awa-a-g_hpafoot1_1!o2&utm_source=google.com&utm_medium=referral&utm_campaign=google_hpafooter&fg=1");
                this.scenario3case2 = false;

            }

            this.goBack();
            element = driver.FindElement(By.XPath(@"//*[@id=""fsl""]/a[2]"));
            element.Click();
            string isBusiness = driver.Url;

            if (!isBusiness.Equals("https://www.google.com/services/?subid=ww-ww-et-g-awa-a-g_hpbfoot1_1!o2&utm_source=google.com&utm_medium=referral&utm_campaign=google_hpbfooter&fg=1#?modal_active=none"))
            {
                log.Info("Business");
                this.scenario3case2 = false;

            }

            this.goBack();
            element = driver.FindElement(By.XPath(@"//*[@id=""fsl""]/a[3]"));
            element.Click();
            string isAbout = driver.Url;

            if (!isAbout.Equals("https://about.google/?utm_source=google-KZ&utm_medium=referral&utm_campaign=hp-footer&fg=1"))
            {
                log.Info("About");
                this.scenario3case2 = false;

            }

            this.goBack();
            element = driver.FindElement(By.XPath(@"//*[@id=""fsl""]/a[4]"));
            element.Click();
            string isHelp = driver.Url;

            if (!isHelp.Equals("https://www.google.com/search/howsearchworks/?fg=1"))
            {
                log.Info("Help");
                this.scenario3case2 = false;

            }

            this.goBack();
            element = driver.FindElement(By.XPath(@"//*[@id=""fsr""]/a[1]"));
            element.Click();
            string isPrivacy = driver.Url;

            if (!isPrivacy.Equals("https://policies.google.com/privacy?fg=1"))
            {
                log.Info("Privacy");
                this.scenario3case2 = false;

            }

            this.goBack();
            element = driver.FindElement(By.XPath(@"//*[@id=""fsr""]/a[2]"));
            element.Click();
            string isTerms = driver.Url;

            if (!isTerms.Equals("https://policies.google.com/terms?fg=1"))
            {
                log.Info("Terms");
                this.scenario3case2 = false;

            }

            this.goBack();
            if (this.scenario3case2 == true)
            {
                log.Info("Test Case: TS_TS3_2, Result: Pass");
            }
            else if (this.scenario3case2 == false)
            {
                log.Info("Test Case: TS_TS3_2, Result: NotPass");
            }
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
        public void TestButtonFunctionality()
        {
            this.goBack();

            log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetAllRepositories().FirstOrDefault();
            FileAppender appender = repository.GetAppenders().OfType<FileAppender>().FirstOrDefault();
            log.DebugFormat("log file located at : {0}", appender.File);
            IWebElement element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[3]/center/input[2]"));

            log.Info("Scenario TS_3:");
            this.Scenario3Case1(element);

            this.goBack();
            this.Scenario3Case2(element);

       
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

    }
}
