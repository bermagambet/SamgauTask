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
    class GoogleTesting2
    {
        private static readonly ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static int starter = 0;

        string testword1 = "Cabbage";
        string testword2 = "Apple";
        string testword3 = "Orange";
        string testword4 = "google.com";
        string testword5 = "Moscow";
        string testword6 = "pppppppp";
        string testword7 = "pdf";

        string[] splitterTestword;

        List<string> testOperators = new List<string>();

        List<string> testRequests = new List<string>();

        IWebDriver driver;
        string googleurl = "http://www.google.com";

        public void initializeOperators()
        {
            testOperators.Add("OR");
            testOperators.Add("AND");
            testOperators.Add("*");
            testOperators.Add("-");//3
            testOperators.Add("( )");
            testOperators.Add("define:"); //5
            testOperators.Add("define:");
            testOperators.Add("cache:");
            testOperators.Add("cache:");
            testOperators.Add("filetype:"); //9
            testOperators.Add("site:");
            testOperators.Add("intitle: allintitle:"); //11
            testOperators.Add("inurl: allinurl:");
            testOperators.Add("intext: allintext:"); //13
            testOperators.Add("weather:"); //14
            testOperators.Add("weather:");
            testOperators.Add("stock:");
            testOperators.Add("stock:");
            testOperators.Add("map:");
            testOperators.Add("map:");
            testOperators.Add("movie:");
            testOperators.Add("movie:"); //16 unique operators in the testing
            testOperators.Add("");
        }
        public void initializeTestData()
        {
            for (int i = 0; i < 23; i++)
            {
                if (i >= 0 && i <= 2)
                {
                    this.testRequests.Add(this.testword1 + " " + this.testOperators[i] + " " + this.testword3);
                }
                if (i == 3 || (i > 4 && i <= 22))
                {
                    if (i == 3)
                    {
                        this.testRequests.Add(this.testword2 + " " + this.testOperators[i] + "iphone");
                    }
                    else if (this.testOperators[i].Equals(this.testOperators[i - 1]))
                    {
                        this.testRequests.Add(this.testOperators[i] + this.testword6);
                    }
                    else if (i == 7 || i == 10)
                    {
                        
                        this.testRequests.Add(this.testOperators[i] + this.testword4);
                    }
                    else if (i == 9)
                    {
                        this.testRequests.Add(this.testword1 + " " + this.testOperators[i] + this.testword7);
                    }
                    else if (i == 11 || i == 12 || i == 13)
                    {
                        this.splitterTestword = this.testOperators[i].Split(' ');
                        this.testRequests.Add(this.splitterTestword[0] + (this.testword4.Split('.'))[0] + " " +
                            this.testOperators[0] + " " + this.splitterTestword[1] + this.testword4);

                    }
                    else if (i == 14)
                    {
                        this.testRequests.Add(this.testOperators[i] + this.testword5);
                    }
                    else if (i == 16 || i == 20 || i == 18)
                    {
                        this.testRequests.Add(this.testOperators[i] + this.testword2);
                    }
                    else if (i == 22)
                    {
                        this.testRequests.Add(this.testOperators[i]);
                    }
                    else if (i == 15)
                    {
                        this.testRequests.Add(this.testOperators[i] + this.testword5);
                    }
                    else
                        this.testRequests.Add(this.testOperators[i] + this.testword1);
                }
                if (i == 4)
                {
                    this.splitterTestword = this.testOperators[i].Split(' ');
                    this.testRequests.Add(splitterTestword[0] + this.testword1 + " " + 
                        this.testOperators[i-1] + this.testword2 + splitterTestword[1] + " " + 
                        this.testOperators[i-2] + " " + this.testword3);
                }
            }
        }
        public void Scenario1Case1to4(IWebElement element)
        {
            element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[1]/div/div[2]/input"));
            List<bool> scenario1cases = new List<bool>();
            //*[@id="rso"]/div[2]/div/div[1]/a/h3
            //*[@id="rso"]/div[1]/div/div[1]/a/h3
            //*[@id="rso"]/div[3]/div/div[1]/a/h3
            //*[@id="rso"]/div[2]/div/div[2]/div[2]
            //*[@id="rso"]/div[2]
            //*[@id="rso"]/div[1]/div/div[1]/a/h3
            //*[@id="rso"]/div[1]
            //*[@id="rso"]/div[3]
            for (int i = 0; i < 23; i++)
            {
                log.Info("Test Case TS_TS_" + i + ": testing...");
                string[] splitterAnswer = (this.testRequests[i].Split(':'));
                /*if (i % 5 == 0 && i != 0 && i > 4)
                {
                    Thread.Sleep(2000);
                    driver = new ChromeDriver("C:\\Program Files (x86)\\Google\\Chrome\\Application\\");
                    driver.Url = this.googleurl;
                }*/

                if (i == 22)
                {
                    if (!driver.Url.Equals(this.googleurl))
                    {
                        scenario1cases[i] = false;
                    }
                    break;
                }


                element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[1]/div/div[2]/input"));
                scenario1cases.Add(true);
                element.SendKeys(this.testRequests[i]);
                element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[3]/center/input[1]"));
                element.Click();
                if (i == 7 || i == 8)
                {
                   
                    if (i == 7)
                    {
                        element = driver.FindElement(By.XPath(@"//*[@id=""bN015htcoyT__google-cache-hdr""]/div[1]/span[1]/a"));
                            this.goBack();
                            continue;
                    }
                    else if (i == 8)
                    {
                        if (driver.Url.Contains(@"http://webcache.googleusercontent.com/search"))
                        {
                            this.goBack();
                            scenario1cases[i] = false;
                            continue;
                        }
                        else
                        {
                            this.goBack();
                            continue;

                        }

                    }

                }

                

                for (int j = 1; j < 11; j++)
                {

                   
                    //*[@id="bN015htcoyT__google-cache-hdr"]/div[1]/span[1]/a
                    //*[@id="bN015htcoyT__google-cache-hdr"]/div[1]/span[1]/a
                    //*[@id="tsuid5"]/span/div/div/div[1]/div/div
                    //*[@id="wob_loc"]
                    //*[@id="rso"]/div[1]/div
                    //*[@id="rso"]/div[1]/div/h2



                    element = driver.FindElement(By.XPath(@"//*[@id=""rso""]/div[" + j + "]"));
                    if (i == 9)
                    {
                        if (element.Text.Contains((this.testRequests[i].Split(' '))[0]) || element.Text.Contains((this.testRequests[i].Split(' '))[0].ToLower()))
                        {
                        }
                        else
                        {
                            scenario1cases[i] = false;
                
                        }
                        }
                    else
                    if (i == 11 || i == 12 || i == 13)
                    {
                        if (j == 5)
                            break;
                        splitterAnswer = (this.testRequests[i].Split(' '));
                        string[] splitterAnswerSpec1 = splitterAnswer[0].Split(':');
                        string[] splitterAnswerSpec2 = splitterAnswer[2].Split(':');
                        if ((element.Text).Contains(splitterAnswerSpec1[splitterAnswerSpec1.Length - 1]) || (element.Text).Contains(splitterAnswerSpec2[splitterAnswerSpec2.Length - 1]) ||
                            (element.Text.Contains("Weather") || element.Text.Contains("Definiton") || 
                            element.Text.Contains("Dictionary") || element.Text.Contains("Movie") || 
                            element.Text.Contains("Map")))
                        {
                        }
                        else
                        {
                            scenario1cases[i] = false;
                            
                        }
                    }
                    else
                    if ((i > 4 && i <= 21) && (i != 9 || i != 11 || i != 12 || i != 13  )) {
                        if (this.testOperators[i].Equals(this.testOperators[i - 1]))
                        {
                            splitterAnswer = (this.testRequests[i-1].Split(':'));
                            if (element.Text.Contains(splitterAnswer[splitterAnswer.Length - 1]))
                            {
                                scenario1cases[i] = false;
                              
                            }
                        }
                        else
                        if ((element.Text).Contains(splitterAnswer[splitterAnswer.Length - 1]) || 
                            ( element.Text.Contains("Weather") || element.Text.Contains("Definiton") || element.Text.Contains("Dictionary") || element.Text.Contains("Movie") || element.Text.Contains("Map")))
                        {
                        }
                        else
                        {
                            scenario1cases[i] = false;
                          
                        }
                    }
                    else if (i == 3)
                    {
                        if ((element.Text).Contains(splitterAnswer[splitterAnswer.Length - 1]))
                        {
                            scenario1cases[i] = false;
                            
                        }
                    }
                    

                }
                if (scenario1cases[i] == false)
                {
                    log.Info("Failed");
                }
                this.goBack();
            }


            log.Info("Current website: " + driver.Url);

            bool answer = true;
            for (int i = 0; i < 23; i++)
            {
                if (scenario1cases[i] != true)
                {
                    answer = false;
                }
            }

            if (answer == true)
                log.Info("Test Scenario: TS_1, Result: Pass");
            else
                log.Info("Test Scenario: TS_1, Result: NotPass");
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
        public void TestSearchFunctionality()
        {
            initializeOperators();
            initializeTestData();
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetAllRepositories().FirstOrDefault();
            FileAppender appender = repository.GetAppenders().OfType<FileAppender>().FirstOrDefault();
            log.DebugFormat("log file located at : {0}", appender.File);

            this.goBack();
            log.Info("Scenario TS_1:");
            IWebElement element = driver.FindElement(By.XPath(@"//*[@id=""tsf""]/div[2]/div[1]/div[3]/center/input[2]"));

            
            this.Scenario1Case1to4(element);

        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
