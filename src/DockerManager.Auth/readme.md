# Running migrations

When you need to add migrations, you need to do the following: 

- cd into the src directory (docker_manager/src)
- run the command `dotnet ef migrations add <migration_name> --project DockerManager.Auth.csproj --startup-project DockerManager.csproj`

This command will create the new migration file in the Migrations folder of the DockerManager.Auth project.
The migration can be used to apply changes to the database schema.

> When running locally, edit the appsettings.json file to point the database to a writable local location, like `c:\docker-manager`(Windows) or `~/docker-manager`(Linux/Mac).
> When running in Docker, the database will be created in the container's file system, which is ephemeral. You should use a persistent volume to store the database data if you want to keep it across container restarts.
>> The volume in docker can be mounted to a local directory on the host machine, like `c:\docker-manager`(Windows) or `~/docker-manager`(Linux/Mac), to keep the data persistent.