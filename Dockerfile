FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["T_PostService/T_PostService.csproj", "T_PostService/"]
RUN dotnet restore "T_PostService/T_PostService.csproj"
COPY . .
WORKDIR "/src/T_PostService"
RUN dotnet build "T_PostService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "T_PostService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "T_PostService.dll"]
