using System.Net;

namespace TruckSystem.Domain.Common
{
    public class BaseResult<TEntity> where TEntity : new()
    {
        public BaseResult()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string Error { get; set; }

        public TEntity Result { get; set; }
    }
}
