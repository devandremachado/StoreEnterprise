FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/app.api/Store.ShoppingCart/Store.ShoppingCart.API/Store.ShoppingCart.API.csproj", "src/app.api/Store.ShoppingCart/Store.ShoppingCart.API/"]
RUN dotnet restore "src/app.api/Store.ShoppingCart/Store.ShoppingCart.API/Store.ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/src/app.api/Store.ShoppingCart/Store.ShoppingCart.API"
RUN dotnet build "Store.ShoppingCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.ShoppingCart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.ShoppingCart.API.dll"]