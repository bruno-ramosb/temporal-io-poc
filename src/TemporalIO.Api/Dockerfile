FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/TemporalIO.Api/TemporalIO.Api.csproj", "src/TemporalIO.Api/"]
RUN dotnet restore "src/TemporalIO.Api/TemporalIO.Api.csproj"
COPY . .
WORKDIR "/src/src/TemporalIO.Api"
RUN dotnet build "TemporalIO.Api.csproj" --configuration Release --no-restore --nologo --output /app/build 

FROM build AS publish
RUN dotnet publish "TemporalIO.Api.csproj" --configuration Release --no-restore --nologo --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TemporalIO.Api.dll"]