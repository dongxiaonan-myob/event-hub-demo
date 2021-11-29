FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EventHubPublisherDemo/EventHubPublisherDemo.csproj", "EventHubPublisherDemo/"]
RUN dotnet restore "EventHubPublisherDemo/EventHubPublisherDemo.csproj"
COPY . .
WORKDIR "/src/EventHubPublisherDemo"
RUN dotnet build "EventHubPublisherDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventHubPublisherDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventHubPublisherDemo.dll"]
