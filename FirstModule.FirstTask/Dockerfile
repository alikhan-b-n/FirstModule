FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FirstModule/FirstModule.csproj", "FirstModule/"]
RUN dotnet restore "FirstModule/FirstModule.csproj"
COPY . .
WORKDIR "/src/FirstModule"
RUN dotnet build "FirstModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirstModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirstModule.dll"]
