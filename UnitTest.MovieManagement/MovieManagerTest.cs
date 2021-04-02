using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PROG8060_Group.Models;
using PROG8060_Group.Models.DB;
using System;
using System.Data;

namespace UnitTest.MovieManagement
{
    [TestFixture]
    public class MovieManagerTest
    {
        public enum TestScenario
        {
            FAIL_BOOL_RETURN = 0,
            PASS_BOOL_RETURN = 1,
            FAIL_OBJECT_RETURN = 2,
            PASS_OBJECT_RETURN = 3
        }

        private MovieManager GetMovieManager(TestScenario scenario)
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
                case TestScenario.PASS_OBJECT_RETURN:
                    var adapterMock = new Mock<IDataAdapter>();
                    var dataSetMock = new Mock<DataSet>();
                    var dataTableMock = new Mock<DataTable>();
                    var dataRowMock = new Mock<DataRow>();
                    connectionFactoryMock.Setup(m => m.CreateDataAdapter(commandMock.Object)).Returns(adapterMock.Object);
                    adapterMock.Setup(m => m.Fill(dataSetMock.Object));
                    break;
            }

            return new MovieManager(connectionFactoryMock.Object);
        }

        [Test]
        public void AddMovieTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_BOOL_RETURN);
            int ret = movieManager.AddMovie(new MovieInfo("title1", "director1", "genere1", "cast1", 2020, "award1"));
            Assert.IsTrue(TypeHelper.IsNumeric(ret.GetType()));

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => movieManager.AddMovie(new MovieInfo("title1", "director1", "genere1", "cast1", 2020, "award1")),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to add movie. Exception of type 'System.Exception' was thrown."));
        }

        [Test]
        public void UpdateMovieTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = movieManager.UpdateMovie(new MovieInfo("title1", "director1", "genere1", "cast1", 2020, "award1"));
            Assert.IsTrue(ret);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => movieManager.UpdateMovie(new MovieInfo("title1", "director1", "genere1", "cast1", 2020, "award1")),
                              Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to update movie. Exception of type 'System.Exception' was thrown."));
        }

        [Test]
        public void DeleteMoviesTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = movieManager.DeleteMovies("1,2,3");
            Assert.IsTrue(ret);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => movieManager.DeleteMovies("1,2,3"),
            Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to delete movie. Exception of type 'System.Exception' was thrown."));
        }

        [Test]
        public void MaskAsOnShowTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_BOOL_RETURN);
            bool ret = movieManager.MarkAsOnShow("1,2,3");
            Assert.IsTrue(ret);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_BOOL_RETURN);
            Assert.That(() => movieManager.MarkAsOnShow("1,2,3"),
            Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to mark as on show. Exception of type 'System.Exception' was thrown."));
        }

        [Test]
        public void GetMoviesByIdsTest()
        {
            //// Pass
            //var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            //MovieInfo[] movieInfos = movieManager.GetMoviesByIds("1,2,3");
            //Assert.AreEqual(movieInfos.Length, 1);

            // Fail
            var movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesByIds("1,2,3"),
            Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to get movie detail by ids, ids: 1,2,3. Dataset table fails"));
        }

        [Test]
        public void GetMoviesByPrefixTest()
        {
            //// Pass
            //var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            //MovieInfo[] movieInfos = movieManager.GetMoviesByPrefix("prefix");
            //Assert.AreEqual(movieInfos.Length, 1);

            // Fail
            var movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesByPrefix("preifx"),
            Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to get movie detail by prefix, prefix: preifx. Dataset table fails"));
        }

        [Test]
        public void GetMoviesOnShowTest()
        {
            //// Pass
            //var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            //MovieInfo[] movieInfos = movieManager.GetMoviesOnShow(true);
            //Assert.AreEqual(movieInfos.Length, 1);

            // Fail
            var movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesOnShow(true),
            Throws.TypeOf<Exception>()
                        .With.Message.EqualTo("Unable to get movie detail, is on show: True. Dataset table fails"));
        }

        [Test]
        public void GetMoviesByPrefixAndOnShowTest()
        {
            //// Pass
            //var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            //MovieInfo[] movieInfos = movieManager.GetMoviesByPrefixAndOnShow("1,2,3", true);
            //Assert.AreEqual(movieInfos.Length, 1);

            // Fail
            var movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesByPrefixAndOnShow("1,2,3", true),
            Throws.TypeOf<Exception>()
                        .With.Message.EqualTo("Unable to get movie detail by prefix, prefix: 1,2,3, is on show: True. Dataset table fails"));
        }

        [Test]
        public void GetMoviesAll()
        {
            //// Pass
            //var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            //MovieInfo[] movieInfos = movieManager.GetMoviesAll();
            //Assert.AreEqual(movieInfos.Length, 1);

            // Fail
            var movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesAll(),
            Throws.TypeOf<Exception>()
                        .With.Message.EqualTo("Unable to get movie (all). Dataset table fails"));

        }
    }
}
