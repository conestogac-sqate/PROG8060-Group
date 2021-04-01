using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG8060_Group.Models
{
    public static class ApiMessageMapping
    {
        public static string SUCCESS = "";
    }

    public class ApiResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public object Data { get; set; }

        public ApiResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            TimeStamp = DateTime.Now;
            Data = data;
        }
    }

    public class ApiSuccess<T> : ApiResult
    {
        public ApiSuccess(T data) : base(true, string.Empty, data) { }
    }

    public class ApiError : ApiResult
    {
        public ApiError(string message) : base(false, message, null) { }
    }
}