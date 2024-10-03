#!/bin/bash
# Esben and Asbjørn fandt ud af dette

# for the DB to be fully startet and ready for changes if any
sleep 60

while ! nc -z user-db 1433; do
  sleep 5
done

cd /app/ApiGateWay
dotnet tool install --global dotnet-ef
/root/.dotnet/tools/dotnet-ef database update