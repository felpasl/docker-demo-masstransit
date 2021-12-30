#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Docker.Demo/Docker.Demo.csproj", "Docker.Demo/"]
RUN dotnet restore "Docker.Demo/Docker.Demo.csproj"
COPY . .
WORKDIR "/src/Docker.Demo"
RUN dotnet build "Docker.Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Demo.dll"]