using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace TruckSystem.WebApi.Controllers
{
    /// <summary>
    /// Controller base para a api
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Monta o resultado
        /// </summary>
        /// <typeparam name="TResult">O tipo do resultado</typeparam>
        /// <param name="result">O resultado</param>
        /// <returns></returns>
        protected IActionResult ApplicationResult(dynamic result, HttpStatusCode Code)
        {
            switch (Code)
            {
                case HttpStatusCode.OK:
                    return Ok(result);
                case HttpStatusCode.BadRequest:
                    return BadRequest(result);
                default:
                    return NotFound(result);
            }
        }
    }
}
