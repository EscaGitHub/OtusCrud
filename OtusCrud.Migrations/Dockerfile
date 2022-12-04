FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OtusCrud.Migrations/OtusCrud.Migrations.csproj", "OtusCrud.Migrations/"]
COPY ["OtusCrud.Data/OtusCrud.Data.csproj", "OtusCrud.Data/"]
RUN dotnet restore "OtusCrud.Migrations/OtusCrud.Migrations.csproj"
COPY . .
WORKDIR "/src/OtusCrud.Migrations"
RUN dotnet build "OtusCrud.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtusCrud.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtusCrud.Migrations.dll"]
