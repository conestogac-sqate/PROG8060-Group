using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG8060_Group.Models;
using PROG8060_Group.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.MovieManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {
        private SessionManager _sessionManager;

        public SessionController()
        {
            _sessionManager = new SessionManager(new MySqlConnectionFactory());
        }

        [HttpPost("Login")]
        public ActionResult Login(string username, string password)
        {
            try
            {
                UserInfo ret = _sessionManager.Login(username, password);
                return Json(new ApiSuccess<UserInfo>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpPost("Logout")]
        public ActionResult Logout(string username)
        {
            try
            {
                bool ret = _sessionManager.Logout(username);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpPost("AddUser")]
        public ActionResult AddUser(string username, string password, string email, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            try
            {
                bool ret = _sessionManager.AddUser(new UserInfo(username, password, email, canCreate, canUpdate, canRead, canDelete));
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpPut("UpdateUser")]
        public ActionResult UpdateUser(string username, string password, string email, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            try
            {
                bool ret = _sessionManager.UpdateUser(new UserInfo(username, password, email, canCreate, canUpdate, canRead, canDelete));
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(string username)
        {
            try
            {
                bool ret = _sessionManager.DeleteUser(username);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"Unable to delete user {username}. {ex.Message}"));
            }
        }

        [Authorize]
        [HttpPut("UpdateUserRole")]
        public ActionResult UpdateUserRole(string username, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            try
            {
                bool ret = _sessionManager.UpdateUserRole(username, canCreate, canUpdate, canRead, canDelete);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("GetUser")]
        public ActionResult GetUser(string username)
        {
            try
            {
                UserInfo ret = _sessionManager.GetUser(username);
                return Json(new ApiSuccess<UserInfo>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("GetUsersAll")]
        public ActionResult GetUsersAll()
        {
            try
            {
                UserInfo[] ret = _sessionManager.GetUsersAll();
                return Json(new ApiSuccess<UserInfo[]>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }
    }
}
