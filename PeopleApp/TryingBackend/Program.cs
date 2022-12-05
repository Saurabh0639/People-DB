using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TryingBackend.Data;
using TryingBackend.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddControllers();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddCors();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "People API", Version = "v1", Description = "API for people" });
    //c.IncludeXmlComments(XmlCommentsFilePath);
});

//////builder.Services.AddSwaggerGen(s =>
//////{
//////    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//////    {
//////        Version = "V1",
//////        Title = "People API",
//////        Description = "API for people"
//////    });
//////    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//////    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//////    s.IncludeXmlComments(xmlPath);
//////});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "People API");
        //c.RoutePrefix = string.Empty;
    });
}

//app.UseHttpsRedirection();

//app.UseCors(MyAllowSpecificOrigins);
app.UseCors(options =>
            options.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
