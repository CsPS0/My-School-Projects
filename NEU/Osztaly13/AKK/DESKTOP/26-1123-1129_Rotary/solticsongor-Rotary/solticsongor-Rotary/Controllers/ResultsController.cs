using Microsoft.AspNetCore.Mvc;
using RotaryLib;

namespace solticsongor_Rotary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResultsController : ControllerBase
{
    readonly IVersenyzoStore _store;

    public ResultsController(IVersenyzoStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IEnumerable<Versenyzo> Get()
    {
        return _store.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Versenyzo> Get(string id)
    {
        Versenyzo? versenyzo = _store.GetById(id);
        if (versenyzo == null)
        {
            return NotFound();
        }
        return versenyzo;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Versenyzo value)
    {
        if (!_store.AddNew(value))
        {
            return BadRequest();
        }
        return Ok();
    }
}
