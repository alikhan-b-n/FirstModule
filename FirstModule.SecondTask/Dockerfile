FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FirstModule.SecondTask/FirstModule.SecondTask.csproj", "FirstModule.SecondTask/"]
RUN dotnet restore "FirstModule.SecondTask/FirstModule.SecondTask.csproj"
COPY . .
WORKDIR "/src/FirstModule.SecondTask"
RUN dotnet build "FirstModule.SecondTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirstModule.SecondTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirstModule.SecondTask.dll"]
