FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Src/Directory.Build.props", "Src/Directory.Build.props"]
COPY ["Src/_configuration", "Src/_configuration"]
COPY ["Src/Clients/Lanre.Clients.Host/Lanre.Clients.Host.csproj", "Src/Clients/Lanre.Clients.Host/"]
COPY ["Src/Data/Lanre.Data/Lanre.Data.csproj", "Src/Data/Lanre.Data/"]
COPY ["Src/Domain/Lanre.Domain/Lanre.Domain.csproj", "Src/Domain/Lanre.Domain/"]
COPY ["Src/Infrastructure/Lanre.Infrastructure/Lanre.Infrastructure.csproj", "Src/Infrastructure/Lanre.Infrastructure/"]
COPY ["Src/Clients/Lanre.Clients.Api/Lanre.Clients.Api.csproj", "Src/Clients/Lanre.Clients.Api/"]
COPY ["Src/Application/Lanre.Application/Lanre.Application.csproj", "Src/Application/Lanre.Application/"]
RUN dotnet restore "Src/Clients/Lanre.Clients.Host/Lanre.Clients.Host.csproj"
COPY . .
WORKDIR "/src/Src/Clients/Lanre.Clients.Host"
RUN dotnet build "Lanre.Clients.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Lanre.Clients.Host.csproj" -c Release -o /app
COPY ["Src/Clients/Lanre.Clients.Host/lanrecert.pfx", "/app"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lanre.Clients.Host.dll"]
