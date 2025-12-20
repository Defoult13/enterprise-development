var builder = DistributedApplication.CreateBuilder(args);

var realEstateAgencyDb = builder
    .AddPostgres("real-estate")
    .AddDatabase("real-estate-db");

var kafka = builder.AddKafka("real-estate-kafka")
    .WithKafkaUI()
    .WithEnvironment("KAFKA_AUTO_CREATE_TOPICS_ENABLE", "true");

var kafkaTopic = builder.AddParameter("KafkaTopicName");

var consumeTimeoutMs = builder.AddParameter("KafkaConsumeTimeoutMs");
var maxDeserializeAttempts = builder.AddParameter("KafkaMaxDeserializeAttempts");
var autoCommitEnabled = builder.AddParameter("KafkaAutoCommitEnabled");

var producerMaxProduceAttempts = builder.AddParameter("KafkaProducerMaxProduceAttempts");
var producerRetryDelayMs = builder.AddParameter("KafkaProducerRetryDelayMs");

var apiHost = builder.AddProject<Projects.RealEstate_Api_Host>("realestate-api-host")
    .WithReference(realEstateAgencyDb, "ConnectionString")
    .WithReference(kafka)
    .WaitFor(realEstateAgencyDb)
    .WaitFor(kafka)
    .WithEnvironment("KafkaConsumer__TopicName", kafkaTopic)
    .WithEnvironment("KafkaConsumer__ConsumeTimeoutMs", consumeTimeoutMs)
    .WithEnvironment("KafkaConsumer__MaxDeserializeAttempts", maxDeserializeAttempts)
    .WithEnvironment("KafkaConsumer__AutoCommitEnabled", autoCommitEnabled);

builder.AddProject<Projects.RealEstate_Generator_Kafka_Host>("realestate-generator-kafka-host")
    .WithReference(kafka)
    .WaitFor(kafka)
    .WithReference(apiHost)
    .WithEnvironment("KafkaProducer__TopicName", kafkaTopic)
    .WithEnvironment("KafkaProducer__MaxProduceAttempts", producerMaxProduceAttempts)
    .WithEnvironment("KafkaProducer__RetryDelayMs", producerRetryDelayMs);

builder.Build().Run();