using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //nhung ai ke thua lop nay deu ke thua method, property, function
    //every controller shares similar properties: [ApiController], [Route("api/[controller]")]
    //inheriting ControllerBase: microsoft
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
        
    }
}