FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 1433
EXPOSE 44172

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY UserMngt.sln ./
COPY UserMngt.Data/*.csproj ./UserMngt.Data/
COPY UserMngt.Core/*.csproj ./UserMngt.Core/
COPY UserMngt.Services/*.csproj ./UserMngt.Services/
COPY UserMngt.Api/*.csproj ./UserMngt.Api/

RUN dotnet restore
COPY . .
WORKDIR /src/UserMngt.Data
RUN dotnet build -c Release -o /app

WORKDIR /src/UserMngt.Core
RUN dotnet build -c Release -o /app

WORKDIR /src/UserMngt.Services
RUN dotnet build -c Release -o /app

WORKDIR /src/UserMngt.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserMngt.Api.dll"]