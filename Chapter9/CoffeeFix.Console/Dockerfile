FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CoffeeFix.Console/CoffeeFix.Console.csproj", "CoffeeFix.Console/"]

RUN dotnet restore "CoffeeFix.Console/CoffeeFix.Console.csproj"
COPY . .

WORKDIR "/src/CoffeeFix.Console"
RUN dotnet build "CoffeeFix.Console.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoffeeFix.Console.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "CoffeeFix.Console.dll", "https://coffeefix20190903060142.azurewebsites.net/"]