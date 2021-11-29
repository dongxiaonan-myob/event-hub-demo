FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EventHubReceiverDemo/EventHubReceiverDemo.csproj", "EventHubReceiverDemo/"]
RUN dotnet restore "EventHubReceiverDemo/EventHubReceiverDemo.csproj"
COPY . .
WORKDIR "/src/EventHubReceiverDemo"
RUN dotnet build "EventHubReceiverDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventHubReceiverDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventHubReceiverDemo.dll"]
