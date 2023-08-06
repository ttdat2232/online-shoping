namespace ShopAPI.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionMiddleWare> logger;

        public ExceptionMiddleWare(RequestDelegate requestDelegate, ILogger<ExceptionMiddleWare> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

       public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var errorMessage = ex.Message;
                switch(ex)
                {
                    case InvalidOperationException:
                    case Application.Exceptions.ApplicationException:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case KeyNotFoundException:
                    case ArgumentException:
                    case NullReferenceException:
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        errorMessage = "Error occurred";
                        break;
                }
                var result = System.Text.Json.JsonSerializer.Serialize(new { Message = errorMessage });
                await response.WriteAsync(result);
            }
        }
    }
}
