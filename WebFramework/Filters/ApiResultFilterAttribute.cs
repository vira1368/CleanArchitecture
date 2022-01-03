using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebFramework.Api;

namespace WebFramework.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                case OkResult okResult:
                    {
                        var apiResult = new ApiResult(true, ResultCode.Success);
                        context.Result = new JsonResult(apiResult);
                        break;
                    }

                case OkObjectResult okObjectResult:
                    {
                        var apiResult = new ApiResult<object>(true, ResultCode.Success, okObjectResult.Value);
                        context.Result = new JsonResult(apiResult);
                        break;
                    }

                case BadRequestResult badRequestResult:
                    {
                        var apiResult = new ApiResult(false, ResultCode.BadRequest);
                        context.Result = new JsonResult(apiResult);
                        break;
                    }

                case NotFoundResult notFoundResult:
                    {
                        var apiResult = new ApiResult(false, ResultCode.RecordNotFound);
                        context.Result = new JsonResult(apiResult);
                        break;
                    }

                case ObjectResult objectResult when objectResult.StatusCode == null:
                    {
                        var apiResult = new ApiResult<object>(true, ResultCode.Success, objectResult.Value);
                        context.Result = new JsonResult(apiResult);
                        break;
                    }
            }
            base.OnResultExecuting(context);
        }
    }
}
