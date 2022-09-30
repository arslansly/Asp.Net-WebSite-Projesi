FROM mcr.microsoft.com/dotnet/aspnet:6.0.4 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebSitem.csproj", "."]
RUN dotnet restore "./WebSitem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebSitem.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "WebSitem.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSitem.dll"]