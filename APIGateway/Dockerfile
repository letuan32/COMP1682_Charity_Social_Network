﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "APIGateway.csproj" .
RUN dotnet restore "APIGateway.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["firebase-credentials.json", "."] 
# Copy credential file to container
ENV FirebaseCredentialFile=firebase-credentials.json
# Set environment variable
ENTRYPOINT ["dotnet", "APIGateway.dll"]
