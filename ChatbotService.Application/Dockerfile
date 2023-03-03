FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChatbotService.Application/ChatbotService.Application.csproj", "ChatbotService.Application/"]
RUN dotnet restore "ChatbotService.Application/ChatbotService.Application.csproj"
COPY . .
WORKDIR "/src/ChatbotService.Application"
RUN dotnet build "ChatbotService.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatbotService.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatbotService.Application.dll"]
