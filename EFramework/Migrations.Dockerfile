# Esben and Asbjørn fandt ud af dette
FROM mcr.microsoft.com/dotnet/sdk:8.0
RUN apt-get update && apt-get install -y netcat-openbsd

WORKDIR /app

COPY . .

RUN chmod +x run-ef-database-update.sh
ENTRYPOINT ["/bin/sh", "/app/run-ef-database-update.sh"]