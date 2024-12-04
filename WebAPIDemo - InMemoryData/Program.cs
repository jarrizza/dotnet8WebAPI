var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();



/* Before app.Run() - Not needed for controller based routing (see controllers)
// Routing

app.MapGet("/shirts", () =>
{
    return "Reading shirts";
});

app.MapGet("/shirts/{id}", (int id) =>
{
    return $"Reading shirt: {id}";
});

app.MapPost("/shirts", () =>
{
    return "Creating a shirt";
});

app.MapPut("/shirts/{id}", (int id) =>
{
    return $"Updating shirt: {id}";
});

app.MapDelete("/shirts/{id}", (int id) =>
{
    return $"Deleting shirt: {id}";
});
*/