using VedettLib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

IVedettAllatStore store = new VedettAllatStore();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/animals", () =>
{
    return Results.Ok(store.GetAll());
}).Produces<VedettAllat[]>();

app.MapGet("api/animalsById/{id}", (int id) =>
{
    VedettAllat? allat = store.GetById(id);
    if (allat != null)
    {
        return Results.Ok(allat);
    }
    return Results.NotFound();
}).Produces<VedettAllat>();

app.MapGet("api/animalsByYear/{year}", (int year) =>
{
    return Results.Ok(store.GetByYear(year));
}).Produces<VedettAllat[]>();

app.MapGet("api/categories", () =>
{
    return Results.Ok(store.GetCategories());
}).Produces<string[]>();

app.MapGet("api/animalsByCategory/{category}", (string category) =>
{
    return Results.Ok(store.GetByCategory(category));
}).Produces<VedettAllat[]>();

app.MapPost("api/animals", (VedettAllat allat) =>
{
    return Results.Ok(store.AddNew(allat));
}).Produces<int>();

app.MapPut("api/animals/{id}", (int id, VedettAllat allat) =>
{
    if (store.Update(id, allat))
    {
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("api/animals/{id}", (int id) =>
{
    if (store.Delete(id))
    {
        return Results.Ok();
    }
    return Results.NotFound();
});

app.Run();
