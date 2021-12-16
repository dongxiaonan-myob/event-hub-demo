FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ReceiverWorker/ReceiverWorker.csproj", "ReceiverWorker/"]
RUN dotnet restore "ReceiverWorker/ReceiverWorker.csproj"
COPY . .
WORKDIR "/src/ReceiverWorker"
RUN dotnet build "ReceiverWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReceiverWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReceiverWorker.dll"]
