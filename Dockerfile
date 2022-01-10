FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StockControl/StockControl.csproj", "StockControl/"]
COPY ["StockControl.Shared/StockControl.Shared.csproj", "StockControl.Shared/"]
RUN dotnet restore "StockControl/StockControl.csproj"
COPY . .
WORKDIR "/src/StockControl"
RUN dotnet build "StockControl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockControl.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "StockControl.dll"]

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .