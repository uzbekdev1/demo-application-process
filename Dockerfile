FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY /src .
RUN dotnet restore "Demo.ApplicationProcess.Api/Demo.ApplicationProcess.Api.csproj"
WORKDIR "/src/Demo.ApplicationProcess.Api"
RUN dotnet build "Demo.ApplicationProcess.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.ApplicationProcess.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.ApplicationProcess.Api.dll"]