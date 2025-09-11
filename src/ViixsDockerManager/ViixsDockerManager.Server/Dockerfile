# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG DOCKER_TAG=bookworm-slim 

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-$DOCKER_TAG AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ViixsDockerManager__viixsdockermanager-db="/app/data/viixsdockermanager.db"
# VOLUME /app/data TODO: check how this works

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-$DOCKER_TAG AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_24.x | bash
RUN apt-get -y install nodejs

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ViixsDockerManager.Server/ViixsDockerManager.Server.csproj", "ViixsDockerManager.Server/"]
COPY ["viixsdockermanager.client/viixsdockermanager.client.esproj", "viixsdockermanager.client/"]
RUN dotnet restore "./ViixsDockerManager.Server/ViixsDockerManager.Server.csproj"
COPY . .
WORKDIR "/src/ViixsDockerManager.Server"
RUN dotnet build "./ViixsDockerManager.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ViixsDockerManager.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN cp -R /src/viixsdockermanager.client/dist/. /app/publish/wwwroot/ #make sure we have a frontend runnig

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ViixsDockerManager.Server.dll"]

