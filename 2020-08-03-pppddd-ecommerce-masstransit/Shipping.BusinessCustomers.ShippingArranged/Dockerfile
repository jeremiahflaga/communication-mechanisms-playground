#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Shipping.BusinessCustomers.ShippingArranged/Shipping.BusinessCustomers.ShippingArranged.csproj", "Shipping.BusinessCustomers.ShippingArranged/"]
COPY ["Shipping.Messages/Shipping.Messages.csproj", "Shipping.Messages/"]
RUN dotnet restore "Shipping.BusinessCustomers.ShippingArranged/Shipping.BusinessCustomers.ShippingArranged.csproj"
COPY . .
WORKDIR "/src/Shipping.BusinessCustomers.ShippingArranged"
RUN dotnet build "Shipping.BusinessCustomers.ShippingArranged.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipping.BusinessCustomers.ShippingArranged.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipping.BusinessCustomers.ShippingArranged.dll"]