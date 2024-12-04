using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPIDemo.Data;
using WebAPIDemo.Filters.OperationFilter;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShirtStoreManagement")));

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
  //  options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");  // Versioning through header
});

// builder.Services.AddEndpointsApiExplorer(); no swagger versioning
builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My WebAPI v1", Version = "version 1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My WebAPI v2", Version = "version 2" });

    c.OperationFilter<AuthorizationHeaderOperationFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
