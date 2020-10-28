#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Sales.Orders.OrderCreated/Sales.Orders.OrderCreated.csproj", "Sales.Orders.OrderCreated/"]
COPY ["Sales.Messages/Sales.Messages.csproj", "Sales.Messages/"]
RUN dotnet restore "Sales.Orders.OrderCreated/Sales.Orders.OrderCreated.csproj"
COPY . .
WORKDIR "/src/Sales.Orders.OrderCreated"
RUN dotnet build "Sales.Orders.OrderCreated.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sales.Orders.OrderCreated.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.Orders.OrderCreated.dll"]