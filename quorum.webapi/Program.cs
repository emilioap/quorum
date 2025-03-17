using quorum.domain.Interfaces;
using quorum.service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IDataRepository, DataRepository>();
builder.Services.AddSingleton<IBillService, BillService>();
builder.Services.AddSingleton<ILegislatorService, LegislatorService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Quorum API",
        Version = "v1",
        Description = "API for managing legislators, bills, and votes",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Emilio Pagnoca",
            Email = "emilio@hotmail.dk",
            Url = new Uri("https://github.com/emilioap")
        }
    });

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
