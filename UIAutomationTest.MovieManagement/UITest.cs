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
        private string PATH_APP = @"\Frontend.MovieManagement\bin\Debug\UI.MovieManagement.exe";
        private const string PATH_WIN_DRIVER = "http://127.0.0.1:4723";

        private WindowsDriver<WindowsElement> Initialize()
        {
            WindowsDriver<WindowsElement> session;
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", Environment.CurrentDirectory + PATH_APP);
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
            WindowsElement eUser = session.FindElementByAccessibilityId("btnUser_text");
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
        public void AddMovieTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eAddUser = session.FindElementByAccessibilityId("btnAddMovie");
            eAddUser.Click();
            session.SwitchTo().Window(session.WindowHandles[0]);

            WindowsElement eTitle = session.FindElementByAccessibilityId("txtTitle");
            eTitle.SendKeys($"title{DateTime.Now.ToString()}");

            WindowsElement eDirector = session.FindElementByAccessibilityId("txtDirector");
            eDirector.SendKeys("Director");

            WindowsElement eGenre = session.FindElementByAccessibilityId("txtGenre");
            eGenre.SendKeys("Genre");

            WindowsElement eCast = session.FindElementByAccessibilityId("txtCast");
            eCast.SendKeys("Cast");

            WindowsElement eYear = session.FindElementByAccessibilityId("txtYear");
            eYear.SendKeys("2021");

            WindowsElement eAward = session.FindElementByAccessibilityId("txtAwards");
            eAward.SendKeys("Award");

            WindowsElement ePlay = session.FindElementByAccessibilityId("rBtnNowPlaying");
            ePlay.Click();

            WindowsElement eSubmit = session.FindElementByAccessibilityId("btnSubmit");
            eSubmit.Click();
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

            session.SwitchTo().Window(session.WindowHandles[0]);
            WindowsElement eYes = session.FindElementByName("Yes");
            eYes.Click();
        }

        [Test]
        public void SearchMovieTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eSearch = session.FindElementByAccessibilityId("txtSearch");
            eSearch.SendKeys("The");

            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);
        }

        [Test]
        public void SearchAdvanceMovieTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eSearch = session.FindElementByAccessibilityId("btnSearchAdvance");
            eSearch.Click();

            session.SwitchTo().Window(session.WindowHandles[0]);
            WindowsElement eYear = session.FindElementByAccessibilityId("txtYear");
            eYear.SendKeys("2021");

            WindowsElement eSearchSubmit = session.FindElementByAccessibilityId("btnSearch");
            eSearchSubmit.Click();
            session.SwitchTo().Window(session.WindowHandles[0]);

            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);
        }

        [Test]
        public void ViewUserTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eUser = session.FindElementByAccessibilityId("btnUser");
            eUser.Click();

            session.SwitchTo().Window(session.WindowHandles[0]);
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);
        }

        [Test]
        public void AddUserTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eUser = session.FindElementByAccessibilityId("btnUser");
            eUser.Click();

            session.SwitchTo().Window(session.WindowHandles[0]);
            WindowsElement eAddUser = session.FindElementByAccessibilityId("colAddUser");
            eAddUser.Click();

            WindowsElement eNewUser = session.FindElementByAccessibilityId("txtUsername");
            eNewUser.SendKeys($"newUser{DateTime.Now.ToString()}");

            WindowsElement eEmail = session.FindElementByAccessibilityId("txtEmail");
            eEmail.SendKeys("abc@abc.com");

            WindowsElement ePassword = session.FindElementByAccessibilityId("txtPassword");
            ePassword.SendKeys("P@ssw0rd");

            WindowsElement ePassword2 = session.FindElementByAccessibilityId("txtPasswordConfirm");
            ePassword2.SendKeys("P@ssw0rd");

            WindowsElement eRole = session.FindElementByAccessibilityId("rBtnAdmin");
            eRole.Click();

            WindowsElement eAddSubmit = session.FindElementByAccessibilityId("btnAddUser");
            eAddSubmit.Click();
        }

        [Test]
        public void EditUserTest()
        {
            string username = "jacqueline"; string password = "P@ssw0rd";
            WindowsDriver<WindowsElement> session = Initialize();
            session = WorkflowLogin(session, username, password);
            Assert.AreEqual(session.WindowHandles.Count, 1);

            // Main Page
            session.SwitchTo().Window(session.WindowHandles.Last());
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eUser = session.FindElementByAccessibilityId("btnUser");
            eUser.Click();

            session.SwitchTo().Window(session.WindowHandles[0]);
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);

            WindowsElement eRow = session.FindElementsByClassName("DataGridRow").ElementAt(9);
            AppiumWebElement eCheckbox = eRow.FindElementByClassName("CheckBox");
            eCheckbox.Click();

            session.SwitchTo().Window(session.WindowHandles[0]);
            WindowsElement eYes = session.FindElementByName("Yes");
            eYes.Click();
            Assert.IsTrue(session.FindElementsByClassName("DataGridRow").Count > 0);
        }
    }
}
