# Set the latest node lts alpine image as base
FROM node:lts-alpine3.17 AS base

# Set the working directory to /app
WORKDIR /app

# Build configuration
ARG CONFIGURATION=development

# Copy the packages
COPY Clients/Angular/package*.json ./

# Install the project dependencies
RUN npm install

# Copy the Angular client
COPY Clients/Angular/ ./

# Build the project
RUN npm run build Angular -- --configuration=${CONFIGURATION}

# Get the latest nginx unprivileged alpine image
FROM nginxinc/nginx-unprivileged:1.23-alpine AS final

# Copy the built application
COPY --from=base /app/dist/angular /usr/share/nginx/html

# Change to non-root user
USER 1000:1000
