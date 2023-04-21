FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY . .

RUN dotnet restore ChatbotRest.sln 
RUN dotnet build ChatbotRest.sln --no-restore -c Release


WORKDIR /app
RUN dotnet publish ChatbotRest.sln -c Release -o out
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "WebHost.dll"]