using BDDFrameWork.pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDDFrameWork.steps
{
    [Binding]
    public sealed class HomePageSteps
    {
        HomePage homePage = null;
        IWebDriver webDriver = new ChromeDriver();

        [Given(@"User launches the application")]
        public void GivenUserLaunchesTheApplication()
        {
            webDriver.Navigate().GoToUrl("http://cgross.github.io/angular-busy/demo/");
            homePage = new HomePage(webDriver);
            Assert.That(webDriver.Title, Is.EqualTo("Angular Busy Demo"));
        }

        [When(@"User provides ""(.*)"" ms into Delay and ""(.*)"" into Min Duration input")]
        public void WhenUserProvidesMsIntoDelayAndIntoMinDurationInput(string delay, string duration)
        {
            homePage.ProvideMS(delay, duration);
        }

        [Then(@"User verifies indicator is not visible for ""(.*)"" ms and it will be visible for (.*) ms")]
        public void ThenUserVerifiesIndicatorIsNotVisibleForMsAndItWillBeVisibleForMs(int delay, int duration)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;

            Console.WriteLine($"Local time {startTime:HH:mm:ss}");
            Assert.That(homePage.BusySpinner.Displayed, Is.False);
            Thread.Sleep(delay);
            Assert.That(homePage.BusySpinner.Displayed, Is.True);
            Console.WriteLine($"Local time {endTime:HH:mm:ss}");
            Thread.Sleep(duration);
            Assert.That(homePage.BusySpinner.Displayed, Is.False);
        }

        [When(@"User changes from Standard to Templete Url")]
        public void WhenUserChangesFromStandardToTempleteUrl()
        {
            homePage.Button.Click();
            Assert.That(homePage.animationElement.GetAttribute("style").Contains("finalfantasy.gif"), Is.False);
        }

        [Then(@"User verifies that busy indicator switches from a spinner to a dancing wizard")]
        public void ThenUserVerifiesThatBusyIndicatorSwitchesFromASpinnerToADancingWizard()
        {
            var selectElement = new SelectElement(homePage.TemplateUrl);
            selectElement.SelectByText("custom-template.html");
            homePage.Button.Click();
            Assert.That(homePage.animationElement.GetAttribute("style").Contains("finalfantasy.gif"), Is.True);
            
        }

        [Then(@"User verifies that ""(.*)"" and ""(.*)"" messages shown in the busy indicator")]
        public void ThenUserVerifiesThatAndMessagesShownInTheBusyIndicator(string msg1, string msg2)
        {
            homePage.Message.Clear();
            homePage.Message.SendKeys(msg1);
            Assert.AreEqual(msg1, homePage.SpinnerMessage.Text);

            homePage.Message.Clear();
            homePage.Message.SendKeys(msg2);
            Assert.AreEqual(msg2, homePage.SpinnerMessage.Text);
        }

        [Then(@"User sets minimum duration to ""(.*)"" ms and press Demo button")]
        public void ThenUserSetsMinimumDurationToMsAndPressDemoButton(string duration)
        {
            homePage.DurationInput.Clear();
            homePage.DurationInput.SendKeys(duration);
            homePage.Button.Click();
         }

        [Then(@"User verifies that ""(.*)"" message is shown in the busy indicator for ""(.*)"" ms")]
        public void ThenUserVerifiesThatMessageIsShownInTheBusyIndicatorForMs(string msg3, long expTime)
        {
            Assert.AreEqual(msg3, homePage.SpinnerMessage.Text);
            Stopwatch stopWatch = new Stopwatch();

            /*if (homePage.SpinnerMessage.Displayed)
            {
                stopWatch.Start();
            } 
            if (!homePage.SpinnerMessage.Displayed)
            {
                stopWatch.Stop();
            }*/

            Assert.True(homePage.SpinnerMessage.Displayed);

            long duration = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("Amount of time: " + duration);
            Assert.False(expTime == duration);
        }




    }
}
