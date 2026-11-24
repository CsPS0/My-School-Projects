using Microsoft.AspNetCore.Mvc;
using VedettLib;

namespace solticsongor_VedettAllatok.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    readonly IVedettAllatStore _store;

    public AnimalsController(IVedettAllatStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IEnumerable<VedettAllat> Get()
    {
        return _store.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<VedettAllat> Get(int id)
    {
        VedettAllat? allat = _store.GetById(id);
        if (allat == null)
        {
            return NotFound();
        }
        return allat;
    }

    [HttpPost]
    public int Post([FromBody] VedettAllat value)
    {
        return _store.AddNew(value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] VedettAllat value)
    {
        if (!_store.Update(id, value))
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_store.Delete(id))
        {
            return NotFound();
        }
        return Ok();
    }
}
