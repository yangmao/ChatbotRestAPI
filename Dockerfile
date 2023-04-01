FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY *.sln .
COPY Chatbot.Domain/*.csproj ./Chatbot.Domain/
COPY Chatbot.Domain.Tests/*.csproj ./Chatbot.Domain.Tests/
COPY ChatbotRestAPI/*.csproj ./ChatbotRestAPI/
COPY ChatbotRestAPI.Tests/*.csproj ./ChatbotRestAPI.Tests/
COPY Database.SQLServer/*.csproj ./Database.SQLServer/
COPY Database.SqlServer.Tests/*.csproj ./Database.SqlServer.Tests/
COPY TrainingDataProviderAPI/*.csproj ./TrainingDataProviderAPI/
COPY WebHost/*.csproj ./WebHost/ 
RUN dotnet restore
COPY Chatbot.Domain/. ./Chatbot.Domain/
COPY ChatbotRestAPI/. ./ChatbotRestAPI/
COPY Database.SQLServer/. ./Database.SQLServer/
COPY TrainingDataProviderAPI/. ./TrainingDataProviderAPI/
COPY WebHost/. ./WebHost/

WORKDIR /app
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "WebHost.dll"]