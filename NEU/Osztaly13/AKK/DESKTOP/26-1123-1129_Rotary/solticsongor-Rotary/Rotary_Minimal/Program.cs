using RotaryLib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

IVersenyzoStore store = new VersenyzoStore();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/results", () =>
{
    return Results.Ok(store.GetAll());
}).Produces<Versenyzo[]>();

app.MapGet("api/results/{id}", (string id) =>
{
    Versenyzo? versenyzo = store.GetById(id);
    if (versenyzo != null)
    {
        return Results.Ok(versenyzo);
    }
    return Results.NotFound();
}).Produces<Versenyzo>();

app.MapPost("api/results", (Versenyzo versenyzo) =>
{
    if (store.AddNew(versenyzo))
    {
        return Results.Ok();
    }
    return Results.BadRequest();
});

app.Run();
