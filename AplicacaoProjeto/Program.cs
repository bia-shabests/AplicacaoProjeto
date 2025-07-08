using AplicacaoProjeto.AppConfig;
using Serilog.Sinks.MSSqlServer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddDependencyInjection();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        })
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy",
        policy => policy
            .WithOrigins("http://localhost:5173") // Porta do Vite!
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Host.UseSerilog();

builder.Configuration.AddUserSecrets<Program>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCustomSwagger();
app.UseAuthorization();
app.MapControllers();
app.UseCors("FrontendPolicy");

await app.RunAsync();
