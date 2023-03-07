FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TDonation/TDonation.csproj", "TDonation/"]
RUN dotnet restore "TDonation/TDonation.csproj"
COPY . .
WORKDIR "/src/TDonation"
RUN dotnet build "TDonation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TDonation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TDonation.dll"]
