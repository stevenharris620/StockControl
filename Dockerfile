FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StockControl.API/StockControl.API.csproj", "StockControl.API/"]
COPY ["StockControl.Shared/StockControl.Shared.csproj", "StockControl.Shared/"]
RUN dotnet restore "StockControl.API/StockControl.API.csproj"
COPY . .
WORKDIR "/src/StockControl.API"
RUN dotnet build "StockControl.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockControl.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockControl.API.dll"]