FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY AcxiomErp/AcxiomErp/AcxiomErp.csproj AcxiomErp/AcxiomErp/
RUN dotnet restore AcxiomErp/AcxiomErp/AcxiomErp.csproj
COPY AcxiomErp/AcxiomErp/ AcxiomErp/AcxiomErp/
RUN dotnet publish AcxiomErp/AcxiomErp/AcxiomErp.csproj -c Release --no-restore -o /out
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_ENVIRONMENT=Production DEMO_MODE=true DEMO_DB="Data Source=/tmp/acxiom-demo.db"
EXPOSE 8080
USER app
CMD ["sh", "-c", "exec dotnet AcxiomErp.dll --urls http://0.0.0.0:${PORT:-8080}"]
