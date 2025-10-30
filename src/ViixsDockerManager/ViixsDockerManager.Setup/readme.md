# ViixsDockerManager.Setup

Contains everything related to create a first time setup experience for a user. This uses the database to save it's setup state.

run `dotnet ef migrations add <MigrationName> --project ViixsDockerManager.Setup --startup-project ViixsDockerManager.Server --context ViixsDockerManager.Setup.DbContexts.SetupWriteDbContext` to create the migrations.