using CommentFormTestProject.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CommentFormTestProject
{
    public class CommentFormTests
    {
        public static IWebDriver driver;
        public string pathToDriver = "C:\\Users\\ljiljana\\Downloads\\"; // Here goes path to chromedriver.exe
        public string url = "https://www.ultimateqa.com/filling-out-forms/";

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            driver = new ChromeDriver(@pathToDriver, options);
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Console.WriteLine("Web page title: " + driver.Title);
        }

        [Test]
        public void TestInvalidCaptcha()
        {
            CommentFormPOM commentForm = new CommentFormPOM(driver);

            string captchaText = commentForm.TxtCaptcha.Text;
            Console.WriteLine("Captcha: " + captchaText);

            commentForm.FillForm("Ljiljana", "This is the first message.", "-1");

            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(driver => commentForm.MsgWrongNumber);
            Console.WriteLine("Message shown: " + commentForm.MsgWrongNumber.Text);

            string newCaptchaText = commentForm.TxtCaptcha.Text;
            Console.WriteLine("New captcha: " + newCaptchaText);

            Assert.AreNotEqual(captchaText, newCaptchaText);
        }

        [Test]
        public void TestValidCaptcha()
        {
            CommentFormPOM commentForm = new CommentFormPOM(driver);

            string captchaText = commentForm.TxtCaptcha.Text;
            Console.WriteLine("Captcha: " + captchaText);

            int sum = commentForm.ReturnNumberFromCaptha(captchaText);

            commentForm.FillForm("Ljiljana", "This is the second message.", sum.ToString());

            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(driver => commentForm.MsgSuccess);
            Console.WriteLine("Message shown: " + commentForm.MsgSuccess.Text);
            
            Assert.AreEqual(commentForm.MsgSuccess.Text, "Success");
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Browser is closed");
        }
    }
}