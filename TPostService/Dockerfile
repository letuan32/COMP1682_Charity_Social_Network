FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "TPostService.csproj" .
RUN dotnet restore "TPostService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "TPostService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TPostService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPostService.dll"]
