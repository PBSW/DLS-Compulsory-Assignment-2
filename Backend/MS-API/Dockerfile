FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Backend/MS-API/MS-API.csproj", "Backend/MS-API/"]
COPY ["Backend/Shared/Shared.csproj", "Backend/Shared/"]
RUN dotnet restore "Backend/MS-API/MS-API.csproj"
COPY . .
WORKDIR "/src/MS-API"
RUN dotnet build "/src/Backend/MS-API/MS-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/Backend/MS-API/MS-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS-API.dll"]
