FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=http://+:8010
WORKDIR /app
EXPOSE 8010
EXPOSE 8009

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Otus.Auth/Otus.Auth.csproj", "Otus.Auth/"]
COPY ["OtusCrud.Data/OtusCrud.Data.csproj", "OtusCrud.Data/"]
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
