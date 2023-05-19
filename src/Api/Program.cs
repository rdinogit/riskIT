using Api.Configuration;
using Api.CQRS;
using Api.Filters;
using Api.Mapping;
using Api.Providers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WordAnalyzer;

var builder = WebApplication.CreateBuilder(args);


// Services -----
builder.Services.AddMapping();
builder.Services.AddProviders();
builder.Services.AddJwtTokenAuthentication(builder.Configuration);
builder.Services.AddWordFrequencyAnalyzers();
builder.Services.AddCqrsHandlers();
builder.Services.AddVersioning();
builder.Services.ConfigureRoutes();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<InvalidWordExceptionFilter>();
    options.Filters.Add<ValidationExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();


// Request Pipeline -----
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
        if (provider is null)
            return;

        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
