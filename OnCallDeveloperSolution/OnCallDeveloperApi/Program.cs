using OnCallDeveloperApi.Adapters;
using OnCallDeveloperApi.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISystemTime, SystemTime>();
builder.Services.AddScoped<DeveloperLookup>();
builder.Services.AddTransient<IProvideTheBusinessClock, StandardBusinessClock>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", (DeveloperLookup developerLookup) =>
{


    OnCallDeveloperResponse response = developerLookup.GetOnCallDeveloper();
        return response;
   
 
});

app.Run();

public partial class Program { }

public record OnCallDeveloperResponse
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}