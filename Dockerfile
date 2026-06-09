# --- ÉTAPE 1 : COMPILATION (BUILD) ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier le fichier projet et restaurer les dépendances
COPY ["Backend/InfiniSoft.Admin.csproj", "Backend/"]
RUN dotnet restore "Backend/InfiniSoft.Admin.csproj"

# Copier tout le code source et publier l'application
COPY . .
WORKDIR "/src/Backend"
RUN dotnet publish "InfiniSoft.Admin.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- ÉTAPE 2 : IMAGE FINALE (RUNTIME) ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Configuration du port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Copier les fichiers compilés du backend (SANS LE CODE SOURCE .CS)
COPY --from=build /app/publish ./Backend

# Copier les fichiers statiques du frontend (HTML, CSS, JS)
COPY frontend/ ./frontend/

# Définition du dossier de travail pour lancer le site
WORKDIR /app/Backend

# Lancement de l'application
ENTRYPOINT ["dotnet", "InfiniSoft.Admin.dll"]
