using Confluent.Kafka;
using RealEstate.Generator.Kafka.Host;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOptions<KafkaProducerSettings>()
    .Bind(builder.Configuration.GetSection("KafkaProducer"));

builder.Services.AddSingleton(sp =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();

    var bootstrapServers = cfg.GetConnectionString("real-estate-kafka");
    if (string.IsNullOrWhiteSpace(bootstrapServers))
        throw new InvalidOperationException("Kafka connection string 'real-estate-kafka' is missing. Ensure AppHost .WithReference(kafka) is configured.");

    var producerConfig = new ProducerConfig
    {
        BootstrapServers = bootstrapServers,
        Acks = Acks.All
    };

    return new ProducerBuilder<Null, string>(producerConfig).Build();
});

builder.Services.AddSingleton<KafkaProducer>();

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

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
