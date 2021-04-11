using Moq;
using NUnit.Framework;
using PROG8060_Group.Models;
using PROG8060_Group.Models.DB;
using System;
using System.Data;

namespace UnitTest.MovieManagement
{
    [TestFixture]
    public class SessionManagerTest
    {
        public enum TestScenario 
        {
            FAIL_BOOL_RETURN = 0,
            PASS_BOOL_RETURN = 1,
            FAIL_OBJECT_RETURN = 2,
            PASS_OBJECT_RETURN = 3
        }

        private SessionManager GetSessionManager(TestScenario scenario)
        {
            var connectionFactoryMock = new Mock<IDbConnectionFactory>();
            var connectionMock = new Mock<IDbConnection>();
            var commandMock = new Mock<IDbCommand>();
            var paramsMock = new Mock<IDataParameterCollection>();
            var paramMock = new Mock<IDbDataParameter>();

            connectionFactoryMock.Setup(m => m.CreateConnection()).Returns(connectionMock.Object);
            connectionMock.Setup(m => m.CreateCommand()).Returns(commandMock.Object);
            commandMock.Setup(m => m.CreateParameter()).Returns(paramMock.Object);
            commandMock.SetupGet(m => m.Parameters).Returns(paramsMock.Object);

            switch (scenario)
            {
                case TestScenario.FAIL_BOOL_RETURN:
                case TestScenario.PASS_BOOL_RETURN:
                    commandMock.Setup(m => m.ExecuteNonQuery()).Verifiable();
                    paramMock.Setup(m => m.Value).Returns((int)scenario % 2);
                    break;
                case TestScenario.FAIL_OBJECT_RETURN:
                    break;
                case TestScenario.PASS_OBJECT_RETURN:
                    var adapterMock = new Mock<IDataAdapter>();
                    var dataSetMock = new Mock<DataSet>();
                    var dataTableMock = new Mock<DataTable>();
                    var dataRowMock = new Mock<DataRow>();
                    connectionFactoryMock.Setup(m => m.CreateDataAdapter(commandMock.Object)).Returns(adapterMock.Object);
                    adapterMock.Setup(m => m.Fill(dataSetMock.Object));
                    break;
            }
            
            return new SessionManager(connectionFactoryMock.Object);
        }

        [Test]
        public void LoginTest()
        {
            // Pass
            var sessionManager = GetSessionManager(TestScenario.PASS_BOOL_RETURN);
            UserInfo ret = sessionManager.Login("testUser1", "P@ssw0rd");
            Assert.AreEqual(ret.Name, "testUser1");

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => sessionManager.Login("testUser1", "P@ssw0rd"),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to login. Exception of type 'System.Exception' was thrown."));

        }

        [Test]
        public void LogoutTest()
        {
            // Pass
            var sessionManager = GetSessionManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = sessionManager.Logout("testUser1");
            Assert.IsTrue(ret);

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => sessionManager.Logout("testUser1"),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to logout. Exception of type 'System.Exception' was thrown."));

        }

        [Test]
        public void AddUserTest()
        {
            // Pass
            var sessionManager = GetSessionManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = sessionManager.AddUser(new UserInfo("name", "email", true, true, true, true));
            Assert.IsTrue(ret);

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => sessionManager.AddUser(new UserInfo("name", "email", true, true, true, true)),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to add user. Exception of type 'System.Exception' was thrown."));

        }

        [Test]
        public void UpdateUserTest()
        {
            // Pass
            var sessionManager = GetSessionManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = sessionManager.UpdateUser(new UserInfo("name", "email", true, true, true, true));
            Assert.IsTrue(ret);

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => sessionManager.UpdateUser(new UserInfo("name", "email", true, true, true, true)),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to update user. Exception of type 'System.Exception' was thrown."));

        }

        [Test]
        public void DeleteUserTest()
        {
            // Pass
            var sessionManager = GetSessionManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = sessionManager.DeleteUser("name");
            Assert.IsTrue(ret);

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => sessionManager.DeleteUser("name"),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to delete user name. Exception of type 'System.Exception' was thrown."));

        }

        [Test]
        public void GetUserTest()
        {
            var sessionManager = GetSessionManager(TestScenario.PASS_OBJECT_RETURN);
            UserInfo ret = sessionManager.GetUser("name");
            Assert.IsNull(ret);

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => sessionManager.GetUser("name"),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to get user name. Object reference not set to an instance of an object."));

        }

        [Test]
        public void GetUsersAllTest()
        {
            // Pass
            var sessionManager = GetSessionManager(TestScenario.PASS_OBJECT_RETURN);
            UserInfo ret = sessionManager.GetUser("name");
            Assert.IsNull(ret);

            // Fail
            sessionManager = GetSessionManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => sessionManager.GetUsersAll(),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to get movie (all)."));

        }
    }
}
