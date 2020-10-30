#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Billing.Payments.PaymentAccepted/Billing.Payments.PaymentAccepted.csproj", "Billing.Payments.PaymentAccepted/"]
COPY ["Billing.Messages/Billing.Messages.csproj", "Billing.Messages/"]
RUN dotnet restore "Billing.Payments.PaymentAccepted/Billing.Payments.PaymentAccepted.csproj"
COPY . .
WORKDIR "/src/Billing.Payments.PaymentAccepted"
RUN dotnet build "Billing.Payments.PaymentAccepted.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billing.Payments.PaymentAccepted.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billing.Payments.PaymentAccepted.dll"]