# Set the .NET 6 SDK image as base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

# Set the working directory to /app
WORKDIR /app

# Copy Budgetify Functions projects
COPY Api/Budgetify.Functions Api/Budgetify.Functions
COPY Api/Budgetify.Common Api/Budgetify.Common
COPY Api/Budgetify.Contracts Api/Budgetify.Contracts
COPY Api/Budgetify.Entities Api/Budgetify.Entities
COPY Api/Budgetify.Services Api/Budgetify.Services
COPY Api/Budgetify.Storage Api/Budgetify.Storage

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
  --self-contained

# Get the lightest dotnet-isolated image
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated6.0-slim AS final

# Set the working directory to /home/site/wwwroot
WORKDIR /home/site/wwwroot

# Copy the published application
COPY --from=base /app/publish .

# Change to non-root user
USER 1000:1000

# Set required environment variable
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
