FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY . /src
WORKDIR /src
RUN dotnet restore --packages /packages
RUN dotnet publish -c Release -o /published

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://*:${PORT}
WORKDIR /app
ENTRYPOINT ["dotnet", "WebHost.dll"]