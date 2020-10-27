#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["eCommerce.Web/eCommerce.Web.csproj", "eCommerce.Web/"]
COPY ["Sales.Messages/Sales.Messages.csproj", "Sales.Messages/"]
RUN dotnet restore "eCommerce.Web/eCommerce.Web.csproj"
COPY . .
WORKDIR "/src/eCommerce.Web"
RUN dotnet build "eCommerce.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Web.dll"]