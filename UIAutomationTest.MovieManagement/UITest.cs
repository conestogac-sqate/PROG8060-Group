using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomationTest.MovieManagement
{
    [TestFixture]
    public class UITest
    {
        private const string PATH_APP = @"C:\Users\Administrator\Desktop\prog8060\PROG8060-Group\Frontend.MovieManagement\bin\Debug\UI.MovieManagement.exe";
        private const string PATH_WIN_DRIVER = "http://127.0.0.1:4723";

        private WindowsDriver<WindowsElement> Initialize()
        {
            WindowsDriver<WindowsElement> session;
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", PATH_APP);
            session = new WindowsDriver<WindowsElement>(new Uri(PATH_WIN_DRIVER), desiredCapabilities);
            return session;
        }

        private WindowsDriver<WindowsElement> WorkflowLogin(WindowsDriver<WindowsElement> session, string username, string password)
        {
            session.SwitchTo().Window(session.WindowHandles.Last());
            WindowsElement eUsername = session.FindElementByAccessibilityId("txtUsername");
            eUsername.SendKeys(username);

            WindowsElement ePassword = session.FindElementByAccessibilityId("txtPassword");
            ePassword.SendKeys(password);

            WindowsElement eLogin = session.FindElementByAccessibilityId("btnLogin");
            eLogin.Click();

            return session;
        }

        [Test]
        public void LoginTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement>  session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);
            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            WindowsElement eUser = session.FindElementByAccessibilityId("btnUser");
            Assert.AreEqual(eUser.Text, username);

            WindowsElement eClose = session.FindElementByName("Close");
            eClose.Click();
        }

        [Test]
        public void ViewMovieTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eClose = session.FindElementByName("Close");
            eClose.Click();
        }

        [Test]
        public void EditMovieTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eEditRow = session.FindElementsByClassName("DataGridRow").ElementAt(0);
            AppiumWebElement eEditButton = eEditRow.FindElementByName("Edit");
            eEditButton.Click();

            session.SwitchTo().Window(session.WindowHandles[0]);
            WindowsElement eEditSubmit = session.FindElementByAccessibilityId("btnSubmit");
            eEditSubmit.Click();

            WindowsElement eSubmitMessage = session.FindElementByName("Movie Submit Success!");
            Assert.AreEqual(eSubmitMessage.Text, "Movie Submit Success!");
        }

        [Test]
        public void DeleteMovieTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eDeleteRow = session.FindElementsByClassName("DataGridRow").ElementAt(0);
            AppiumWebElement eDeleteButton = eDeleteRow.FindElementByName("Delete");
            eDeleteButton.Click();

            string page = session.PageSource;
            WindowsElement eSubmitMessage = session.FindElementByName("Movie Submit Success!");
            Assert.AreEqual(eSubmitMessage.Text, "Movie Submit Success!");
        }
    }
}
