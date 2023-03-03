FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChatbotService.Application.WebApi/ChatbotService.Application.WebApi.csproj", "ChatbotService.Application.WebApi/"]
RUN dotnet restore "ChatbotService.Application.WebApi/ChatbotService.Application.WebApi.csproj"
COPY . .
WORKDIR "/src/ChatbotService.Application.WebApi"
RUN dotnet build "ChatbotService.Application.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatbotService.Application.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatbotService.Application.WebApi.dll"]
