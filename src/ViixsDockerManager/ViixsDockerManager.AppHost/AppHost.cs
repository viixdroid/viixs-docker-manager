var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Projects.ViixsDockerManager_Server>("viixsdockermanager-server");

builder.AddNpmApp("viixsdockermanager-client", "../viixsdockermanager.client", scriptName: "dev")
       .WithReference(backend)
       .WithEndpoint(targetPort: 55596, scheme: "https", isExternal: true)
       .PublishAsDockerFile();

builder.Build().Run();
