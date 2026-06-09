# 🚀 Infini-Soft - Site Web Complet (Docker)

Bienvenue sur le dépôt officiel du site Infini-Soft. Ce projet contient une solution complète comprenant le **Site Vitrine (Frontend)** et le **Portail d'Administration (Backend)**, le tout prêt à être déployé via Docker.

## 🛡️ Protection du Code
Le code source est entièrement compilé à l'intérieur de l'image Docker pour garantir la sécurité et la propriété intellectuelle. Vous pouvez utiliser le site et le déployer sans avoir accès aux fichiers sources `.cs`.

## 📦 Pré-requis
- **Docker** et **Docker Compose** installés sur votre machine.
- Un accès à internet pour télécharger l'image depuis Docker Hub.

## 🚀 Installation Rapide

1. **Récupérer le fichier de configuration :**
   Vous n'avez besoin que du fichier `docker-compose.yml` présent à la racine de ce dépôt.

2. **Configurer vos accès (Fichier .env) :**
   Créez un fichier nommé `.env` dans le même dossier que votre `docker-compose.yml` et ajoutez les deux mots de passe fournis par l'administrateur :
   ```env
   DB_PASSWORD=votre_mot_de_passe_base_de_donnees
   JWT_SECRET=votre_cle_secrete_admin
   ```

3. **Lancer le site :**
   Ouvrez un terminal dans le dossier et tapez :
   ```bash
   docker-compose up -d
   ```

4. **Accéder au site :**
   Le site est désormais accessible sur : **[http://localhost:8080](http://localhost:8080)**

## ⚙️ Détails Techniques
- **Image Docker** : [chaimaayadi/web_vitrine:latest](https://hub.docker.com/r/chaimaayadi/web_vitrine) (Docker Hub)
- **Port** : 8080
- **Base de données** : Connexion ultra-rapide via **Supabase Connection Pooler** (PostgreSQL) pour une performance optimale.

---
© 2026 Infini-Soft. Tous droits réservés.
