using PROG8060_Group.Models;
using PROG8060_Group.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROG8060_Group.Controllers
{
    public class SessionController : JsonController
    {
        private SessionManager _sessionManager;

        public SessionController()
        {
            _sessionManager = new SessionManager(new MySqlConnectionFactory());
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            SecurityManager.Authorize(Request);
            try
            {
                bool ret = _sessionManager.Login(username, password);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }

        [HttpPost]
        public ActionResult Logout(string username)
        {
            SecurityManager.Authorize(Request);
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

        [HttpPost]
        public ActionResult AddUser(string username, string password, string email, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            SecurityManager.Authorize(Request);
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

        [HttpPut]
        public ActionResult UpdateUser(string username, string password, string email, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            SecurityManager.Authorize(Request);
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

        [HttpDelete]
        public ActionResult DeleteUser(string username)
        {
            SecurityManager.Authorize(Request);
            try
            {
                bool ret = _sessionManager.DeleteUser(username);
                return Json(new ApiSuccess<bool>(ret));
            }
            catch(Exception ex)
            {
                return Json(new ApiError($"Unable to delete user {username}. {ex.Message}"));
            }
       }

        [HttpGet]
        public ActionResult GetUser(string username)
        {
            SecurityManager.Authorize(Request);
            try
            {
                UserInfo ret = _sessionManager.GetUser(username);
                return new JsonResult()
                {
                    Data = new ApiSuccess<UserInfo>(ret),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return Json(new ApiError($"{ex.Message}"));
            }
        }
    }
}