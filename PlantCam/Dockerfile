#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-arm32v7 AS build
WORKDIR /src
COPY ["PlantCam.csproj", "PlantCam/"]
RUN dotnet restore "PlantCam/PlantCam.csproj"
COPY . .
WORKDIR "/src/PlantCam"
COPY . .
RUN dotnet build "PlantCam.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlantCam.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlantCam.dll"]