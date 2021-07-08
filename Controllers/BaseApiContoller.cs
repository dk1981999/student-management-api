using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace student_management_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
    }
}
