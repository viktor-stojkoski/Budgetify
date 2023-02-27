# #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/azure-functions/dotnet:4-slim AS base
# WORKDIR /home/site/wwwroot
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# ARG PRIVATE_FEED_TOKEN
# ARG PRIVATE_FEED=https://nuget.pkg.github.com/viktor-stojkoski/index.json
# RUN dotnet nuget add source $PRIVATE_FEED \
#   --name github \
#   --username Docker \
#   --password $PRIVATE_FEED_TOKEN \
#   --store-password-in-clear-text \
#   --valid-authentication-types "basic"
# COPY ["Api/Budgetify.Functions/Budgetify.Functions.csproj", "Api/Budgetify.Functions/"]
# COPY ["Api/Budgetify.Services/Budgetify.Services.csproj", "Api/Budgetify.Services/"]
# COPY ["Api/Budgetify.Common/Budgetify.Common.csproj", "Api/Budgetify.Common/"]
# COPY ["Api/Budgetify.Storage/Budgetify.Storage.csproj", "Api/Budgetify.Storage/"]
# COPY ["Api/Budgetify.Contracts/Budgetify.Contracts.csproj", "Api/Budgetify.Contracts/"]
# COPY ["Api/Budgetify.Entities/Budgetify.Entities.csproj", "Api/Budgetify.Entities/"]
# RUN dotnet restore "Api/Budgetify.Functions/Budgetify.Functions.csproj"
# COPY . .
# WORKDIR "/src/Api/Budgetify.Functions"
# RUN dotnet build "Budgetify.Functions.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "Budgetify.Functions.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /home/site/wwwroot
# COPY --from=publish /app/publish .
# ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
#   AzureFunctionsJobHost__Logging__Console__IsEnabled=true

# Set the Azure Functions .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base

# Set the working directory to /app
WORKDIR /app

# Copy Budgetify Functions projects
COPY Api/Budgetify.Services Api/Budgetify.Services
COPY Api/Budgetify.Functions Api/Budgetify.Functions
COPY Api/Budgetify.Common Api/Budgetify.Common
COPY Api/Budgetify.Storage Api/Budgetify.Storage
COPY Api/Budgetify.Contracts Api/Budgetify.Contracts
COPY Api/Budgetify.Entities Api/Budgetify.Entities

# Private feed credentials
ARG PRIVATE_FEED_TOKEN
ARG PRIVATE_FEED=https://nuget.pkg.github.com/viktor-stojkoski/index.json
RUN dotnet nuget add source $PRIVATE_FEED \
  --name github \
  --username Docker \
  --password $PRIVATE_FEED_TOKEN \
  --store-password-in-clear-text \
  --valid-authentication-types "basic"

# Publish functions to /app/publish
RUN dotnet publish Api/Budgetify.Functions/Budgetify.Functions.csproj \
  --configuration Release \
  --output /app/publish \
  --runtime alpine-x64 \
  --self-contained

# Get the alpine linux image
FROM mcr.microsoft.com/azure-functions/dotnet:4-slim AS final

# Set the working directory to /app
WORKDIR /home/site/wwwroot

# Copy the published application
COPY --from=base /app/publish ./

# Change to non-root user
USER 1000:1000

ENV AzureWebJobsScriptRoot=/home/site/wwwroot
