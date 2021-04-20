using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG8060_Group.Models;
using PROG8060_Group.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG8060_Group.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private MovieManager _movieManager;

        public MovieController()
        {
            _movieManager = new MovieManager(new MySqlConnectionFactory());
        }

        [Authorize]
        [HttpPost("Add")]
        public ActionResult Add(string title, string director, string genre, string cast, int year, string award)
        {
            try
            {
                int ret = _movieManager.AddMovie(new MovieInfo(title, director, genre, cast, year, award));
                return Json(new ApiSuccess<int>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public ActionResult Update(int movieId, string title, string director, string genre, string cast, int year, string award, bool is_show)
        {
            try
            {
                bool ret = _movieManager.UpdateMovie(new MovieInfo(movieId, title, director, genre, cast, year, award, is_show));
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public ActionResult Delete(string ids)
        {
            try
            {
                bool ret = _movieManager.DeleteMovies(ids);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("GetByIds")]
        public ActionResult GetByIds(string ids)
        {
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesByIds(ids);
                return Json(new ApiSuccess<MovieInfo[]>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("GetByPrefix")]
        public ActionResult GetByPrefix(string prefix)
        {
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesByPrefix(prefix);
                return Json(new ApiSuccess<MovieInfo[]>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("GetByAdvanceSearch")]
        public ActionResult GetByAdvanceSearch(string title, string director, string genre, string cast, int year, string award, int isOnShow)
        {
            try
            {
                MovieInfo[] ret = _movieManager.GetByAdvanceSearch(new SearchConfiguration(title, director, genre, cast, year, award, (SearchConfiguration.OnShow)isOnShow));
                return Json(new ApiSuccess<MovieInfo[]>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("GetMoviesAll")]
        public ActionResult GetMoviesAll()
        {
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesAll();
                return Json(new ApiSuccess<MovieInfo[]>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }
    }
}
