using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult PublicEndpoint() => Ok(new { message = "Nyilvános végpont" });

    [Authorize]
    [HttpGet("protected")]
    public IActionResult ProtectedEndpoint() => Ok(new { message = "Sikeres hitelesítés" });
}