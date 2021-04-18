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
        public void GetMoviesByIdsTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            MovieInfo[] movieInfos = movieManager.GetMoviesByIds("1,2,3");
            Assert.IsNull(movieInfos);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesByIds("1,2,3"),
            Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to get movie detail by ids, ids: 1,2,3."));
        }

        [Test]
        public void GetMoviesByPrefixTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            MovieInfo[] movieInfos = movieManager.GetMoviesByPrefix("prefix");
            Assert.IsNull(movieInfos);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesByPrefix("preifx"),
            Throws.TypeOf<Exception>()
                                    .With.Message.EqualTo("Unable to get movie detail by prefix, prefix: preifx."));
        }

        [Test]
        public void GetByAdvanceSearchTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            MovieInfo[] movieInfos = movieManager.GetByAdvanceSearch(new SearchConfiguration());
            Assert.IsNull(movieInfos);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetByAdvanceSearch(new SearchConfiguration()),
            Throws.TypeOf<Exception>()
                        .With.Message.EqualTo("Unable to get movie detail by search configuration."));
        }

        [Test]
        public void GetMoviesAllTest()
        {
            // Pass
            var movieManager = GetMovieManager(TestScenario.PASS_OBJECT_RETURN);
            MovieInfo[] movieInfos = movieManager.GetMoviesAll();
            Assert.IsNull(movieInfos);

            // Fail
            movieManager = GetMovieManager(TestScenario.FAIL_OBJECT_RETURN);
            Assert.That(() => movieManager.GetMoviesAll(),
            Throws.TypeOf<Exception>()
                        .With.Message.EqualTo("Unable to get movie (all)."));

        }
    }
}
