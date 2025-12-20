var builder = DistributedApplication.CreateBuilder(args);

var realEstateAgencyDb = builder
    .AddPostgres("real-estate")
    .AddDatabase("real-estate-db");

var kafka = builder.AddKafka("real-estate-kafka")
    .WithKafkaUI();

var kafkaTopic = builder.AddParameter("KafkaTopicName");
var consumeTimeoutMs = builder.AddParameter("KafkaConsumeTimeoutMs");
var maxDeserializeAttempts = builder.AddParameter("KafkaMaxDeserializeAttempts");
var autoCommitEnabled = builder.AddParameter("KafkaAutoCommitEnabled");

builder.AddProject<Projects.RealEstate_Api_Host>("realestate-api-host")
    .WithReference(realEstateAgencyDb, "ConnectionString")
    .WithReference(kafka)
    .WaitFor(realEstateAgencyDb)
    .WaitFor(kafka)
    .WithEnvironment("KafkaConsumer__TopicName", kafkaTopic)
    .WithEnvironment("KafkaConsumer__ConsumeTimeoutMs", consumeTimeoutMs)
    .WithEnvironment("KafkaConsumer__MaxDeserializeAttempts", maxDeserializeAttempts)
    .WithEnvironment("KafkaConsumer__AutoCommitEnabled", autoCommitEnabled);

builder.Build().Run();