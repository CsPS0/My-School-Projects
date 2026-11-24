using Microsoft.AspNetCore.Mvc;
using VedettLib;

namespace solticsongor_VedettAllatok.Controllers;

[Route("api")]
[ApiController]
public class VedettAllatLekeresController : ControllerBase
{
    readonly IVedettAllatStore _store;

    public VedettAllatLekeresController(IVedettAllatStore store)
    {
        _store = store;
    }

    [HttpGet("animalsById/{id}")]
    public ActionResult<VedettAllat> GetById(int id)
    {
        VedettAllat? allat = _store.GetById(id);
        if (allat == null)
        {
            return NotFound();
        }
        return allat;
    }

    [HttpGet("animalsByYear/{year}")]
    public IEnumerable<VedettAllat> GetByYear(int year)
    {
        return _store.GetByYear(year);
    }

    [HttpGet("animalsByCategory/{category}")]
    public IEnumerable<VedettAllat> GetByCategory(string category)
    {
        return _store.GetByCategory(category);
    }
}
