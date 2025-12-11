using Microsoft.EntityFrameworkCore;
using RealEstate.Application;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateObjects;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Application.Services;
using RealEstate.Domain;
using RealEstate.Domain.DataSeeder;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.EfCore;
using RealEstate.Infrastructure.EfCore.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new RealEstateMappingProfile());
});

builder.Services.AddSingleton<RealEstateDataSeeder>();

builder.Services.AddScoped<IRepository<Counterparty, int>, CounterpartyRepository>();
builder.Services.AddScoped<IRepository<RealEstateObject, int>, RealEstateObjectRepository>();
builder.Services.AddScoped<IRepository<RealEstateRequest, int>, RealEstateRequestRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<ICounterpartyService, CounterpartyService>();
builder.Services.AddScoped<IRealEstateObjectService, RealEstateObjectService>();
builder.Services.AddScoped<IApplicationService<RealEstateRequestDto, RealEstateRequestCreateUpdateDto, int>, RealEstateRequestService>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("RealEstate"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

    c.UseInlineDefinitionsForEnums();
});

builder.AddNpgsqlDbContext<RealEstateDbContext>("ConnectionString");

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<RealEstateDbContext>();

        await context.Database.MigrateAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
