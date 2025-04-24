using AplicacaoProjeto.AppConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddDependencyInjection();

builder.Configuration.AddUserSecrets<Program>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCustomSwagger();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
