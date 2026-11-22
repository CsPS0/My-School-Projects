using Microsoft.AspNetCore.Mvc;
using solticsongor_VedettAllatok.Models;

namespace solticsongor_VedettAllatok.Controllers;

[ApiController]
[Route("api")]
public class AnimalsController : ControllerBase
{
    [HttpGet("animals")]
    public IActionResult GetAll()
    {
        return Ok(AnimalData.Animals);
    }

    [HttpGet("animalsById/{id}")]
    public IActionResult GetById(int id)
    {
        Animal? animal = AnimalData.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound();
        }
        return Ok(animal);
    }

    [HttpGet("animalsByYear/{year}")]
    public IActionResult GetByYear(int year)
    {
        List<Animal> result = AnimalData.Animals.Where(a => a.Year == year).ToList();
        return Ok(result);
    }

    [HttpGet("categories")]
    public IActionResult GetCategories()
    {
        List<string> result = AnimalData.Animals.Select(a => a.Category).Distinct().OrderBy(c => c).ToList();
        return Ok(result);
    }

    [HttpGet("animalsByCategory/{category}")]
    public IActionResult GetByCategory(string category)
    {
        List<Animal> result = AnimalData.Animals.Where(a => a.Category == category).ToList();
        return Ok(result);
    }

    [HttpPost("animals")]
    public IActionResult Create(Animal animal)
    {
        int newId = 1;
        if (AnimalData.Animals.Count > 0)
        {
            newId = AnimalData.Animals.Max(a => a.Id) + 1;
        }
        animal.Id = newId;
        AnimalData.Animals.Add(animal);
        return Ok(animal);
    }

    [HttpPut("animals/{id}")]
    public IActionResult Update(int id, Animal newData)
    {
        Animal? animal = AnimalData.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound();
        }
        animal.Name = newData.Name;
        animal.Value = newData.Value;
        animal.Year = newData.Year;
        animal.Category = newData.Category;
        return Ok(animal);
    }

    [HttpDelete("animals/{id}")]
    public IActionResult Delete(int id)
    {
        Animal? animal = AnimalData.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound();
        }
        AnimalData.Animals.Remove(animal);
        return Ok();
    }
}
