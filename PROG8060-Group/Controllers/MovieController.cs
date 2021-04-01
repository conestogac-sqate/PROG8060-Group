using Newtonsoft.Json;
using PROG8060_Group.Models;
using PROG8060_Group.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROG8060_Group.Controllers
{
    public class MovieController : Controller
    {
        private MovieManager _movieManager;

        public MovieController()
        {
            _movieManager = new MovieManager(new MySqlConnectionFactory());
        }

        [HttpPost]
        public ActionResult Add(string title, string director, string genre, string cast, int year, string award)
        {
            SecurityManager.Authorize(Request);
            try
            {
                bool ret = _movieManager.AddMovie(new MovieInfo(title, director, genre, cast, year, award));
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpPut]
        public ActionResult Update(int movieId, string title, string director, string genre, string cast, int year, string award)
        {
            SecurityManager.Authorize(Request);
            try
            {
                bool ret = _movieManager.UpdateMovie(new MovieInfo(movieId, title, director, genre, cast, year, award));
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpDelete]
        public ActionResult Delete(string ids)
        {
            SecurityManager.Authorize(Request);
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

        [HttpPost]
        public ActionResult MarkAsOnShow(string ids)
        {
            SecurityManager.Authorize(Request);
            try
            {
                bool ret = _movieManager.MarkAsOnShow(ids);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpGet]
        public ActionResult GetByIds(string ids)
        {
            SecurityManager.Authorize(Request);
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesByIds(ids);
                return new JsonResult()
                {
                    Data = new ApiSuccess<MovieInfo[]>(ret),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpGet]
        public ActionResult GetByPrefix(string prefix)
        {
            SecurityManager.Authorize(Request);
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesByPrefix(prefix);
                return new JsonResult()
                {
                    Data = new ApiSuccess<MovieInfo[]>(ret),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpGet]
        public ActionResult GetByOnShow(bool isOnShow)
        {
            SecurityManager.Authorize(Request);
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesOnShow(isOnShow);
                return new JsonResult()
                {
                    Data = new ApiSuccess<MovieInfo[]>(ret),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpGet]
        public ActionResult GetByPrefixOnShow(string prefix, bool isOnShow)
        {
            SecurityManager.Authorize(Request);
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesByPrefixAndOnShow(prefix, isOnShow);
                return new JsonResult()
                {
                    Data = new ApiSuccess<MovieInfo[]>(ret),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpGet]
        public ActionResult GetMoviesAll()
        {
            SecurityManager.Authorize(Request);
            try
            {
                MovieInfo[] ret = _movieManager.GetMoviesAll();
                return new JsonResult()
                {
                    Data = new ApiSuccess<MovieInfo[]>(ret),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch(Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }
    }
}