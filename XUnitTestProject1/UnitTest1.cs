using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1 : IDisposable
    {
        public IWebDriver driver;


        [Fact(DisplayName = "Given the webpage can be accessed")]
        public void Test1()
        {

            ChromeDriver driver = new ChromeDriver(@"C:\Program Files (x86)\drivers");
            driver.Navigate().GoToUrl("https://www.fly-go.ro/");
            driver.Manage().Window.Maximize();

            //implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //explicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='log-in-box']")))));

            IWebElement contNou = driver.FindElement(By.XPath("//a[@data-target='#modalRegister']"));
            IWebElement prenume = driver.FindElement(By.XPath("//form[@action='/login/']/descendant::input[@name='firstName']"));
            IWebElement nume = driver.FindElement(By.XPath("//form[@action='/login/']/descendant::input[@name='lastName']"));
            IWebElement email = driver.FindElement(By.XPath("//form[@action='/login/']/div[@class='row']/div[@class='col-sm-6']/div[@class='form-group']/input[@name='email'][1]"));
            IWebElement telefonPrefix = driver.FindElement(By.XPath("//div[@class='col-sm-6']//input[@type='text'][@name='phone[1]']"));
            IWebElement telefonNumar = driver.FindElement(By.XPath("//div[@class='col-sm-6']/div[@class='form-group']/div[@class='phone-group']/input[@type='text'][@name='phone[2]'][@placeholder='Telefon']"));
            IWebElement parola = driver.FindElement(By.XPath("//input[@type='password'][@id='i-password']"));
            IWebElement confirmareParola = driver.FindElement(By.XPath("//input[@type='password'][@name='passwordCheck']"));
            IWebElement submitContNou = driver.FindElement(By.XPath("//*[@name='submit'][@value='Cont Nou']"));
            IWebElement termsOfAgreement = driver.FindElement(By.XPath("//div[@class='col-xs-12 col-sm-8']//input[@type='checkbox'][@name='terms']"));

            //select 'Romania' from dropdown
            //IWebElement telefonTaraDropdown = driver.FindElement(By.XPath("//div[@class='phone-group']/select[@name='phone[0]'][@class='form-control first valid']"));
            //IWebElement telefonTaraDropdown = driver.FindElement(By.XPath("//div[@class='phone-group']/select[contains(@name, 'phone[0]')][@class='form-control first valid']"));
            IWebElement country = driver.FindElement(By.Name("phone[0]"));
            SelectElement telefonTara = new SelectElement(country);
            //telefonTara.SelectByText("Romania (40)");

            //select 'Dl' from dropdown
            IWebElement sexDropdown = driver.FindElement(By.XPath("//select[@name='sex']"));
            SelectElement sexOption = new SelectElement(sexDropdown);
            //sexOption.SelectByText("Dl");

            //select '19' from dropdown
            IWebElement dobDropdownDay = driver.FindElement(By.XPath("//select[@name='dob-day']"));
            SelectElement dataNasterii = new SelectElement(dobDropdownDay);
            //dataNasterii.SelectByText("19");

            //select '4' from dropdown
            IWebElement dobDropdownMonth = driver.FindElement(By.XPath("//select[@name='dob-month']"));
            SelectElement lunaNasterii = new SelectElement(dobDropdownMonth);
            //lunaNasterii.SelectByText("4");

            //select '1989' from dropdown -- version 2
            SelectElement dobDropdownYear = new SelectElement(driver.FindElement(By.XPath("//select[@name='dob-year']")));
            //dobDropdownYear.SelectByText("1989");


            contNou.Click();
            prenume.SendKeys("John");
            nume.SendKeys("Smith");
            email.SendKeys("flygo@mailinator.com");
            telefonTara.SelectByValue("40");
            telefonPrefix.SendKeys("040");
            telefonNumar.SendKeys("123456");
            parola.SendKeys("TEST1");
            confirmareParola.SendKeys("TEST1");
            sexOption.SelectByText("Dl");
            dataNasterii.SelectByText("19");
            lunaNasterii.SelectByText("4");
            dobDropdownYear.SelectByText("1989");
            termsOfAgreement.Click();
            submitContNou.Click();

            driver.Navigate().GoToUrl("http://flygoro.flygogroup.com/login/?type=successfullActivation");

            IWebElement loginTwoEmail = driver.FindElement(By.XPath("//div[@class='body default clearfix']/form[@class='login-form validate-form']/div[@class='form-group']/input[@type='email'][@name='username']"));
            IWebElement loginTwoPassword = driver.FindElement(By.XPath("//div[@class='body default clearfix']/form[@class='login-form validate-form']/div[@class='form-group']/input[@type='password'][@name='password']"));
            IWebElement loginTwoLogin = driver.FindElement(By.XPath("//div[@class='body default clearfix']/form[@class='login-form validate-form']/button[@type='submit']"));

            loginTwoEmail.SendKeys("flygo@mailinator.com");
            loginTwoPassword.SendKeys("TEST1");
            loginTwoLogin.Click();

            //IWebElement failMessage = driver.FindElement(By.XPath("//div[@class='body default clearfix red']"));
            var loginMessage = driver.FindElement(By.XPath("//h3[@class='red no-margin m-btm']")).Text;
            Assert.Equal("Ne pare rau...", loginMessage);


        }

        public void Dispose()
        {
            try
            {
                driver.Close();
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while stopping chrome..." + e);
            }
        }



    }

}

