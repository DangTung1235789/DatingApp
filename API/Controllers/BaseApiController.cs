using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //12. Adding an action filter
    [ServiceFilter(typeof(LogUserActivity))]
    //nhung ai ke thua lop nay deu ke thua method, property, function
    //every controller shares similar properties: [ApiController], [Route("api/[controller]")]
    //inheriting ControllerBase: microsoft
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
        
    }
}