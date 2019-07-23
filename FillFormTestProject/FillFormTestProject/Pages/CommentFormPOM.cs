using OpenQA.Selenium;
using System;

namespace CommentFormTestProject.Pages
{
    class CommentFormPOM
    {
        private IWebDriver _driver;

        public CommentFormPOM(IWebDriver driver) => _driver = driver;

        public IWebElement ContactName => _driver.FindElement(By.Id("et_pb_contact_name_1"));

        public IWebElement ContactMessage => _driver.FindElement(By.Id("et_pb_contact_message_1"));

        public IWebElement TxtCaptcha => _driver.FindElement(By.ClassName("et_pb_contact_captcha_question"));

        public IWebElement InputCaptcha => _driver.FindElement(By.Name("et_pb_contact_captcha_1"));

        public IWebElement BtnSubmit => _driver.FindElement(By.XPath("//*[@id='et_pb_contact_form_1']/div[2]/form/div/button"));

        public IWebElement MsgWrongNumber => _driver.FindElement(By.XPath("//*[@id='et_pb_contact_form_1']/div[1]/ul/li"));

        public IWebElement MsgSuccess => _driver.FindElement(By.XPath("//*[@id='et_pb_contact_form_1']/div/p"));

        public int ReturnNumberFromCaptha(string captchaText)
        {
            int firstNumber = Int32.Parse(captchaText.Split("+")[0]);
            int secondNumber = Int32.Parse(captchaText.Split("+")[1]);
            return (firstNumber + secondNumber);
        }

        public void FillForm(string name, string message, string captchaAnswer)
        {
            ContactName.SendKeys(name);
            Console.WriteLine("Entered name: " + ContactName.GetAttribute("value"));
            ContactMessage.SendKeys(message);
            Console.WriteLine("Entered message: " + ContactMessage.GetAttribute("value"));
            InputCaptcha.SendKeys(captchaAnswer);
            Console.WriteLine("Number entered in captcha: " + InputCaptcha.GetAttribute("value"));
            BtnSubmit.Click();
            Console.WriteLine("Submit button clicked");
        }
    }
}
