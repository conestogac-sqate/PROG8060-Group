using PROG8060_Group.Models.DB;
using System;
using System.Data;
using System.Linq;

namespace PROG8060_Group.Models
{
    public class MovieManager
    {
        private IDbConnectionFactory _connectionFactory;

        public MovieManager(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool AddMovie(MovieInfo movieInfo)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_add_movie";

                        IDbDataParameter pTitle = command.CreateParameter();
                        pTitle.ParameterName = "@iTitle";
                        pTitle.Value = movieInfo.Title;
                        command.Parameters.Add(pTitle);

                        IDbDataParameter pDirector = command.CreateParameter();
                        pDirector.ParameterName = "@iDirector";
                        pDirector.Value = movieInfo.Director;
                        command.Parameters.Add(pDirector);

                        IDbDataParameter pGenere = command.CreateParameter();
                        pGenere.ParameterName = "@iGenre";
                        pGenere.Value = movieInfo.Genere;
                        command.Parameters.Add(pGenere);

                        IDbDataParameter pCast = command.CreateParameter();
                        pCast.ParameterName = "@iCast";
                        pCast.Value = movieInfo.Cast;
                        command.Parameters.Add(pCast);

                        IDbDataParameter pYear = command.CreateParameter();
                        pYear.ParameterName = "@iYear";
                        pYear.Value = movieInfo.Year;
                        command.Parameters.Add(pYear);

                        IDbDataParameter pAward = command.CreateParameter();
                        pAward.ParameterName = "@iAward";
                        pAward.Value = movieInfo.Award;
                        command.Parameters.Add(pAward);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }
                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add movie. {ex.Message}");
            }
            return true;
        }

        public bool UpdateMovie(MovieInfo movieInfo)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_update_movie";

                        IDbDataParameter pId = command.CreateParameter();
                        pId.ParameterName = "@iMovieId";
                        pId.Value = movieInfo.Id;
                        command.Parameters.Add(pId);

                        IDbDataParameter pTitle = command.CreateParameter();
                        pTitle.ParameterName = "@iTitle";
                        pTitle.Value = movieInfo.Title;
                        command.Parameters.Add(pTitle);

                        IDbDataParameter pDirector = command.CreateParameter();
                        pDirector.ParameterName = "@iDirector";
                        pDirector.Value = movieInfo.Director;
                        command.Parameters.Add(pDirector);

                        IDbDataParameter pGenere = command.CreateParameter();
                        pGenere.ParameterName = "@iGenre";
                        pGenere.Value = movieInfo.Genere;
                        command.Parameters.Add(pGenere);

                        IDbDataParameter pCast = command.CreateParameter();
                        pCast.ParameterName = "@iCast";
                        pCast.Value = movieInfo.Cast;
                        command.Parameters.Add(pCast);

                        IDbDataParameter pYear = command.CreateParameter();
                        pYear.ParameterName = "@iYear";
                        pYear.Value = movieInfo.Year;
                        command.Parameters.Add(pYear);

                        IDbDataParameter pAward = command.CreateParameter();
                        pAward.ParameterName = "@iAward";
                        pAward.Value = movieInfo.Award;
                        command.Parameters.Add(pAward);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }
                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update movie. {ex.Message}");
            }
        }

        public bool DeleteMovies(string movieIds)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_delete_movies";

                        IDbDataParameter pMovieIds = command.CreateParameter();
                        pMovieIds.ParameterName = "@iMovieIds";
                        pMovieIds.Value = movieIds;
                        command.Parameters.Add(pMovieIds);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }
                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete movie. {ex.Message}");
            }
        }

        public bool MarkAsOnShow(string movieIds)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_movie_mark_as_on_show";

                        IDbDataParameter pMovieIds = command.CreateParameter();
                        pMovieIds.ParameterName = "@iMovieIds";
                        pMovieIds.Value = movieIds;
                        command.Parameters.Add(pMovieIds);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }
                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to mark as on show. {ex.Message}");
            }
        }

        public MovieInfo[] GetMoviesByIds(string movieIds)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_movies_by_ids";

                        IDbDataParameter pMovieIds = command.CreateParameter();
                        pMovieIds.ParameterName = "@iMovieIds";
                        pMovieIds.Value = movieIds;
                        command.Parameters.Add(pMovieIds);

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                if (dataSet == null || dataSet.Tables.Count != 1) { throw new Exception("Dataset table fails"); }
                DataTable dataTable = dataSet.Tables[0];
                MovieInfo[] movieInfos = (from rw in dataTable.AsEnumerable()
                                       select new MovieInfo(Convert.ToInt32(rw["id"]), 
                                                            Convert.ToString(rw["title"]),
                                                            Convert.ToString(rw["director"]),
                                                            Convert.ToString(rw["genre"]),
                                                            Convert.ToString(rw["cast"]),
                                                            Convert.ToInt32(rw["year"]),
                                                            Convert.ToString(rw["award"]),
                                                            Convert.ToBoolean(rw["is_on_show"]))
                                       { }).ToArray();
                if (movieInfos == null || movieInfos.Length == 0) throw new Exception();
                return movieInfos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get movie detail by ids, ids: {movieIds}. {ex.Message}");
            }
        }

        public MovieInfo[] GetMoviesByPrefix(string prefix)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_movies_by_prefix";

                        IDbDataParameter pPrefix = command.CreateParameter();
                        pPrefix.ParameterName = "@iPrefix";
                        pPrefix.Value = prefix;
                        command.Parameters.Add(pPrefix);

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                if (dataSet == null || dataSet.Tables.Count != 1) { throw new Exception("Dataset table fails"); }
                DataTable dataTable = dataSet.Tables[0];

                MovieInfo[] movieInfos = (from rw in dataTable.AsEnumerable()
                                          select new MovieInfo(Convert.ToInt32(rw["id"]),
                                                               Convert.ToString(rw["title"]),
                                                               Convert.ToString(rw["director"]),
                                                               Convert.ToString(rw["genre"]),
                                                               Convert.ToString(rw["cast"]),
                                                               Convert.ToInt32(rw["year"]),
                                                               Convert.ToString(rw["award"]),
                                                               Convert.ToBoolean(rw["is_on_show"]))
                                          { }).ToArray();
                if (movieInfos == null || movieInfos.Length == 0) throw new Exception();

                return movieInfos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get movie detail by prefix, prefix: {prefix}. {ex.Message}");
            }
        }

        public MovieInfo[] GetMoviesOnShow(bool isOnShow)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_movies_on_show";

                        IDbDataParameter pOnShow = command.CreateParameter();
                        pOnShow.ParameterName = "@iOnShow";
                        pOnShow.Value = isOnShow;
                        command.Parameters.Add(pOnShow);

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                if (dataSet == null || dataSet.Tables.Count != 1) { throw new Exception("Dataset table fails"); }
                DataTable dataTable = dataSet.Tables[0];

                MovieInfo[] movieInfos = (from rw in dataTable.AsEnumerable()
                                          select new MovieInfo(Convert.ToInt32(rw["id"]),
                                                               Convert.ToString(rw["title"]),
                                                               Convert.ToString(rw["director"]),
                                                               Convert.ToString(rw["genre"]),
                                                               Convert.ToString(rw["cast"]),
                                                               Convert.ToInt32(rw["year"]),
                                                               Convert.ToString(rw["award"]),
                                                               Convert.ToBoolean(rw["is_on_show"]))
                                          { }).ToArray();
                if (movieInfos == null || movieInfos.Length == 0) throw new Exception();

                return movieInfos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get movie detail, is on show: {isOnShow}. {ex.Message}");
            }
        }

        public MovieInfo[] GetMoviesByPrefixAndOnShow(string prefix, bool isOnShow)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_movies_by_prefix_on_show";

                        IDbDataParameter pPrefix = command.CreateParameter();
                        pPrefix.ParameterName = "@iPrefix";
                        pPrefix.Value = isOnShow;
                        command.Parameters.Add(pPrefix);

                        IDbDataParameter pOnShow = command.CreateParameter();
                        pOnShow.ParameterName = "@iOnShow";
                        pOnShow.Value = isOnShow;
                        command.Parameters.Add(pOnShow);

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                if (dataSet == null || dataSet.Tables.Count != 1) { throw new Exception("Dataset table fails"); }
                DataTable dataTable = dataSet.Tables[0];

                MovieInfo[] movieInfos = (from rw in dataTable.AsEnumerable()
                                          select new MovieInfo(Convert.ToInt32(rw["id"]),
                                                               Convert.ToString(rw["title"]),
                                                               Convert.ToString(rw["director"]),
                                                               Convert.ToString(rw["genre"]),
                                                               Convert.ToString(rw["cast"]),
                                                               Convert.ToInt32(rw["year"]),
                                                               Convert.ToString(rw["award"]),
                                                               Convert.ToBoolean(rw["is_on_show"]))
                                          { }).ToArray();
                if (movieInfos == null || movieInfos.Length == 0) throw new Exception();

                return movieInfos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get movie detail by prefix, prefix: {prefix}, is on show: {isOnShow}. {ex.Message}");
            }
        }

        public MovieInfo[] GetMoviesAll()
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_movies_all";

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                if (dataSet == null || dataSet.Tables.Count != 1) { throw new Exception("Dataset table fails"); }
                DataTable dataTable = dataSet.Tables[0];

                MovieInfo[] movieInfos = (from rw in dataTable.AsEnumerable()
                                          select new MovieInfo(Convert.ToInt32(rw["id"]),
                                                               Convert.ToString(rw["title"]),
                                                               Convert.ToString(rw["director"]),
                                                               Convert.ToString(rw["genre"]),
                                                               Convert.ToString(rw["cast"]),
                                                               Convert.ToInt32(rw["year"]),
                                                               Convert.ToString(rw["award"]),
                                                               Convert.ToBoolean(rw["is_on_show"]))
                                          { }).ToArray();
                if (movieInfos == null || movieInfos.Length == 0) throw new Exception();

                return movieInfos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get movie (all). {ex.Message}");
            }
        }
    }
}