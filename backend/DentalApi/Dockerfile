# ============================================
# ESTÁGIO 1: BUILD
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar o arquivo .csproj e restaurar dependências
COPY DentalApi.csproj .
RUN dotnet restore

# Copiar todo o código e publicar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ============================================
# ESTÁGIO 2: RUNTIME
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Expor as portas
EXPOSE 80
EXPOSE 443

# Copiar os arquivos publicados do estágio de build
COPY --from=build /app/publish .

# Iniciar a aplicação
ENTRYPOINT ["dotnet", "DentalApi.dll"]