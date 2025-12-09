var builder = DistributedApplication.CreateBuilder(args);

var realEstateAgencyDb = builder
    .AddPostgres("real-estate")
    .AddDatabase("real-estate-db");

builder.AddProject<Projects.RealEstate_Api_Host>("realestate-api-host")
    .WithReference(realEstateAgencyDb, "ConnectionString")
    .WaitFor(realEstateAgencyDb);

builder.Build().Run();