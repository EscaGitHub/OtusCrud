FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=http://+:8000
WORKDIR /app
EXPOSE 8001
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OtusCrud/OtusCrud.csproj", "OtusCrud/"]
RUN dotnet restore "OtusCrud/OtusCrud.csproj"
COPY . .
WORKDIR "/src/OtusCrud"
RUN dotnet build "OtusCrud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtusCrud.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtusCrud.dll"]
