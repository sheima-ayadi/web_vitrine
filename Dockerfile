# Étape 1 : Construction du Backend
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier le fichier projet et restaurer les dépendances
COPY ["Backend/InfiniSoft.Admin.csproj", "Backend/"]
RUN dotnet restore "Backend/InfiniSoft.Admin.csproj"

# Copier tout le code source du backend
COPY Backend/ Backend/

# Compiler et publier le backend
WORKDIR "/src/Backend"
RUN dotnet publish "InfiniSoft.Admin.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Étape 2 : Image de runtime finale (légère et sécurisée)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Exposer le port sur lequel l'application écoute
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Copier les fichiers binaires compilés du backend (pas de code source .cs ici)
COPY --from=build /app/publish ./Backend

# Copier les fichiers statiques du frontend (HTML, CSS, JS, Images)
# On garde la structure pour que le backend puisse les servir via ".."
COPY css/ ./css/
COPY img/ ./img/
COPY js/ ./js/
COPY sections/ ./sections/
COPY *.html ./

# Définir le dossier de travail sur Backend pour lancer l'application
WORKDIR /app/Backend

# Lancer l'application
ENTRYPOINT ["dotnet", "InfiniSoft.Admin.dll"]
