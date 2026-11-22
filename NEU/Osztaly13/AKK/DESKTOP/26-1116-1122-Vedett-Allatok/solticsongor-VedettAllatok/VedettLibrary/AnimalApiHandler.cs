using System.Net.Http.Json;

namespace VedettLibrary;

public class AnimalApiHandler
{
    HttpClient client;

    public AnimalApiHandler(string baseUrl)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
    }

    public async Task<List<Animal>> GetAnimals()
    {
        List<Animal>? animals = await client.GetFromJsonAsync<List<Animal>>("api/animals");
        return animals ?? new List<Animal>();
    }

    public async Task<Animal?> GetAnimalById(int id)
    {
        HttpResponseMessage response = await client.GetAsync("api/animalsById/" + id);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        return await response.Content.ReadFromJsonAsync<Animal>();
    }

    public async Task<List<Animal>> GetAnimalsByYear(int year)
    {
        List<Animal>? animals = await client.GetFromJsonAsync<List<Animal>>("api/animalsByYear/" + year);
        return animals ?? new List<Animal>();
    }

    public async Task<List<string>> GetCategories()
    {
        List<string>? categories = await client.GetFromJsonAsync<List<string>>("api/categories");
        return categories ?? new List<string>();
    }

    public async Task<List<Animal>> GetAnimalsByCategory(string category)
    {
        List<Animal>? animals = await client.GetFromJsonAsync<List<Animal>>("api/animalsByCategory/" + Uri.EscapeDataString(category));
        return animals ?? new List<Animal>();
    }

    public async Task<Animal?> AddAnimal(Animal animal)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("api/animals", animal);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        return await response.Content.ReadFromJsonAsync<Animal>();
    }

    public async Task<bool> UpdateAnimal(Animal animal)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync("api/animals/" + animal.Id, animal);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAnimal(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync("api/animals/" + id);
        return response.IsSuccessStatusCode;
    }
}
