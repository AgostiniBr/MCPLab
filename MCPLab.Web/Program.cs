var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles(); // <- ESSENCIAL

app.MapFallbackToFile("index.html"); // <- ESSENCIAL

app.Run();
