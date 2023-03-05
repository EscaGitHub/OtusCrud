FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Otus.Auth/Otus.Auth.csproj", "Otus.Auth/"]
RUN dotnet restore "Otus.Auth/Otus.Auth.csproj"
COPY . .
WORKDIR "/src/Otus.Auth"
RUN dotnet build "Otus.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otus.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otus.Auth.dll"]
