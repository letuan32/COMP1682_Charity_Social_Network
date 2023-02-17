FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TCharity.Post/TCharity.Post.csproj", "TCharity.Post/"]
RUN dotnet restore "TCharity.Post/TCharity.Post.csproj"
COPY . .
WORKDIR "/src/TCharity.Post"
RUN dotnet build "TCharity.Post.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TCharity.Post.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCharity.Post.dll"]
