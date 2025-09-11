using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ViixsDockerManager");
const string databaseFileName = "viixsdockermanager.db";
const string logLocationFileName = "vdm.log";

var sqlite = builder.AddSqlite("viixsdockermanager-db", folderPath, databaseFileName)
    .WithSqliteWeb();

var backend = builder.AddProject<Projects.ViixsDockerManager_Server>("viixsdockermanager-server")
    .WithReference(sqlite)
    .WithEnvironment("ViixsDockerManager__viixsdockermanager-db", $"{Path.Combine(folderPath, databaseFileName)}")
    .WithEnvironment("ViixsDockerManager__logfilelocation", $"{Path.Combine(folderPath, logLocationFileName)}")
    ;

builder.AddNpmApp("viixsdockermanager-client", "../viixsdockermanager.client", scriptName: "dev")
    .WithReference(backend)
    .WithEndpoint(targetPort: 55596, scheme: "https", isExternal: true)
    .PublishAsDockerFile();

builder.Build().Run();
