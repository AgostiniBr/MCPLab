//--> Init Builder
var builder = WebApplication.CreateBuilder(args);

//--> Init App
var app = builder.Build();
app.UseStaticFiles(); // <- ESSENCIAL
app.MapFallbackToFile("index.html"); // <- ESSENCIAL

//-- App Run
app.Run();
