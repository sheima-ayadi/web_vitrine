# 🚀 Infini-Soft - Image Finale Docker

Ce dossier contient tout le nécessaire pour construire et publier l'image Docker de votre site complet (Frontend + Backend) sur GitHub.

## 📁 Structure du dossier
- `Backend/` : Code source C# (utilisé uniquement pour la compilation).
- `frontend/` : Site vitrine statique.
- `Dockerfile` : Recette de compilation sécurisée (ne contient pas le code source dans l'image finale).
- `docker-compose.yml` : Fichier à partager avec les utilisateurs.

## 🛠️ Instructions pour VOUS (Publication)

1. **Construire l'image :**
   ```bash
   docker build -t ghcr.io/sheima-ayadi/web_vitrine:latest .
   ```

2. **Se connecter à GitHub Packages :**
   ```bash
   docker login ghcr.io -u sheima-ayadi
   ```

3. **Publier l'image :**
   ```bash
   docker push ghcr.io/sheima-ayadi/web_vitrine:latest
   ```

## 👥 Instructions pour vos UTILISATEURS

Donnez-leur uniquement le fichier `docker-compose.yml` et dites-leur de :

1. Créer un fichier `.env` à côté du `docker-compose.yml` :
   ```env
   DB_PASSWORD=votre_mot_de_passe_base_de_donnees
   JWT_SECRET=votre_cle_secrete_admin
   ```

2. Lancer le site :
   ```bash
   docker-compose up -d
   ```

Le site sera accessible sur `http://localhost:8080`.
