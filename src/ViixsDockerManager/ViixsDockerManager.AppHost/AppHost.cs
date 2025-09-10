var builder = DistributedApplication.CreateBuilder(args);

var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ViixsDockerManager");
const string fileName = "viixsdockermanager.db";

var sqlite = builder.AddSqlite("viixsdockermanager-db", folderPath, fileName)
    .WithSqliteWeb();

var backend = builder.AddProject<Projects.ViixsDockerManager_Server>("viixsdockermanager-server")
    .WithReference(sqlite)
    .WithEnvironment("ViixsDockerManager__viixsdockermanager-db", $"{Path.Combine(folderPath, fileName)}");

builder.AddNpmApp("viixsdockermanager-client", "../viixsdockermanager.client", scriptName: "dev")
    .WithReference(backend)
    .WithEndpoint(targetPort: 55596, scheme: "https", isExternal: true)
    .PublishAsDockerFile();

builder.Build().Run();
