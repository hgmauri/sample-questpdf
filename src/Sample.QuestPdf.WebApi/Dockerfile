FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Sample.QuestPdf.WebApi/Sample.QuestPdf.WebApi.csproj", "src/Sample.QuestPdf.WebApi/"]
COPY ["src/Sample.QuestPdf.WebApi.Core/Sample.QuestPdf.WebApi.Core.csproj", "src/Sample.QuestPdf.WebApi.Core/"]
RUN dotnet restore "src/Sample.QuestPdf.WebApi/Sample.QuestPdf.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Sample.QuestPdf.WebApi"
RUN dotnet build "Sample.QuestPdf.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.QuestPdf.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.QuestPdf.WebApi.dll"]