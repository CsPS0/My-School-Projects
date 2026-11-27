using System.Net;
using System.Text.Json;
using RestSharp;

namespace Test;

public record Book(int Id, string Title, string Description, int PageCount, string Excerpt, DateTime PublishDate);

public class Tests
{
    private RestClient Client = null!;
    private RestRequest Request = null!;
    private RestResponse Response = null!;

    string baseUrl = "https://fakerestapi.azurewebsites.net";

    [SetUp]
    public void Setup()
    {
        Client = new RestClient(baseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        Client.Dispose();
    }

    [Test]
    public void BaseUrl()
    {
        Assert.That(Client.Options.BaseUrl, Is.EqualTo(new Uri(baseUrl)));
    }

    [Test]
    public void Connection()
    {
        // the root redirects (301) to the Swagger UI at /index.html
        Request = new RestRequest("", Method.Get);
        Response = Client.Execute(Request);
        Assert.That(Response, Is.Not.Null);
        Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(Response.ResponseUri!.AbsolutePath, Is.EqualTo("/index.html"));
    }

    [Test]
    public void SwaggerJson()
    {
        Request = new RestRequest("swagger/v1/swagger.json", Method.Get);
        Response = Client.Execute(Request);
        Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        using var doc = JsonDocument.Parse(Response.Content!);
        var root = doc.RootElement;
        Assert.That(root.GetProperty("info").GetProperty("title").GetString(), Is.EqualTo("FakeRESTApi.Web V1"));
        var paths = root.GetProperty("paths");
        Assert.That(paths.TryGetProperty("/api/v1/Books", out _), Is.True);
        Assert.That(paths.TryGetProperty("/api/v1/Books/{id}", out _), Is.True);
    }

    [Test]
    public void GetAllTest()
    {
        Request = new RestRequest("api/v1/Books", Method.Get);
        var response = Client.Execute<List<Book>>(Request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.ContentType, Does.Contain("application/json"));
        Assert.That(response.Data, Has.Count.EqualTo(200));
        Assert.That(response.Data![0].Id, Is.EqualTo(1));
    }

    [Test]
    public void GetTest()
    {
        Request = new RestRequest("api/v1/Books/1", Method.Get);
        var response = Client.Execute<Book>(Request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Id, Is.EqualTo(1));
        Assert.That(response.Data.Title, Is.EqualTo("Book 1"));
        Assert.That(response.Data.PageCount, Is.GreaterThan(0));
    }

    [Test]
    public void GetNotFoundTest()
    {
        Request = new RestRequest("api/v1/Books/300", Method.Get);
        Response = Client.Execute(Request);
        Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void DeleteTest()
    {
        // the fake API does not persist deletes, so only the status code is checked
        Request = new RestRequest("api/v1/Books/201", Method.Delete);
        Response = Client.Execute(Request);
        Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void GetARandomTest()
    {
        int num = Random.Shared.Next(1, 201);
        Request = new RestRequest($"api/v1/Books/{num}", Method.Get);
        var response = Client.Execute<Book>(Request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data!.Id, Is.EqualTo(num));
        Assert.That(response.Data.Title, Is.EqualTo($"Book {num}"));
    }
}
