using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebFramework.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public ResultCode Code { get; set; }

        public string Message { get; set; }

        public ApiResult(bool isSuccess, ResultCode code, string message = null)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
        }

        //public static implicit operator ApiResult(OkResult result)
        //{
        //    return new ApiResult(true, ResultCode.Success);
        //}

        //public static implicit operator ApiResult(BadRequestResult result)
        //{
        //    return new ApiResult(false, ResultCode.BadRequest);
        //}

        //public static implicit operator ApiResult(NotFoundResult result)
        //{
        //    return new ApiResult(false, ResultCode.RecordNotFound);
        //}
    }

    public class ApiResult<TData> : ApiResult where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, ResultCode code, TData data, string message = null) : base(isSuccess, code, message)
        {
            Data = data;
        }

        //public static implicit operator ApiResult<TData>(TData data)
        //{
        //    return new ApiResult<TData>(true, ResultCode.Success, data);
        //}

        //public static implicit operator ApiResult<TData>(OkResult result)
        //{
        //    return new ApiResult<TData>(true, ResultCode.Success, null);
        //}

        //public static implicit operator ApiResult<TData>(OkObjectResult result)
        //{
        //    return new ApiResult<TData>(true, ResultCode.Success, (TData)result.Value);
        //}

        //public static implicit operator ApiResult<TData>(BadRequestResult result)
        //{
        //    return new ApiResult<TData>(false, ResultCode.BadRequest, null);
        //}

        //public static implicit operator ApiResult<TData>(NotFoundResult result)
        //{
        //    return new ApiResult<TData>(false, ResultCode.RecordNotFound, null);
        //}
    }
}
