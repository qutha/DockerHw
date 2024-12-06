var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.MapGet("/", () => "Hello from Docker!");
app.MapGet("/rad/{deg:double}", (double deg) => Math.PI * deg / 180.0);

app.Run();

// xUnit
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program;