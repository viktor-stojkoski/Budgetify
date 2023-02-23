# Set the .NET SDK 6.0 alpine image as base
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base

# Set the working directory to /app
WORKDIR /app

# Copy Budgetify API
COPY Api/ Api/

# Private feed credentials
ARG PRIVATE_FEED_TOKEN
ARG PRIVATE_FEED=https://nuget.pkg.github.com/viktor-stojkoski/index.json
RUN dotnet nuget add source $PRIVATE_FEED \
  --name github \
  --username Docker \
  --password $PRIVATE_FEED_TOKEN \
  --store-password-in-clear-text \
  --valid-authentication-types "basic"

# Publish .NET application to /app/publish
RUN dotnet publish Api/Budgetify.Api/Budgetify.Api.csproj \
  --configuration Release \
  --output /app/publish \
  --os linux \
  --self-contained

# Get the alpine linux image
FROM alpine:3.16 AS final
# RUN apk add --no-cache \
#   libstdc++ libintl icu tzdata krb5-libs libgcc libssl1.1 zlib
# Change directory to /app
WORKDIR /app

# Copy the published application
COPY --from=base /app/publish ./

# Change to non-root user
# USER 1000:1000
# RUN pwd
# RUN ls
# RUN ls
EXPOSE 55555
# Entrypoint
ENTRYPOINT ["./Budgetify.Api"]
