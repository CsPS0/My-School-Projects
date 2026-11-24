using Microsoft.AspNetCore.Mvc;
using VedettLib;

namespace solticsongor_VedettAllatok.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    readonly IVedettAllatStore _store;

    public CategoriesController(IVedettAllatStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return _store.GetCategories();
    }
}
