-- Script SQL pour Supabase (PostgreSQL)
-- Société : Infini-Soft

-- 1. Table des Utilisateurs (Admin)
CREATE TABLE IF NOT EXISTS "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Username" TEXT NOT NULL UNIQUE,
    "PasswordHash" TEXT NOT NULL
);

-- 2. Table des Articles du Blog
CREATE TABLE IF NOT EXISTS "BlogPosts" (
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "Tag" TEXT,
    "Image" TEXT,
    "Date" TEXT,
    "CreatedAt" TIMESTAMPTZ DEFAULT NOW()
);

-- 3. Table des Services
CREATE TABLE IF NOT EXISTS "Services" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Icon" TEXT,
    "Description" TEXT
);

-- 4. Table des Messages de Contact
CREATE TABLE IF NOT EXISTS "ContactMessages" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Phone" TEXT,
    "Company" TEXT,
    "Subject" TEXT,
    "Message" TEXT NOT NULL,
    "CreatedAt" TIMESTAMPTZ DEFAULT NOW(),
    "IsRead" BOOLEAN DEFAULT FALSE
);

-- 5. Table des Statistiques du Site
CREATE TABLE IF NOT EXISTS "SiteStatistics" (
    "Id" SERIAL PRIMARY KEY,
    "Type" TEXT NOT NULL, -- 'Visit', 'ServiceClick', etc.
    "Value" TEXT,
    "Date" TIMESTAMPTZ DEFAULT NOW()
);

-- Insertion d'un utilisateur par défaut (admin / admin123 - Hash simulé pour l'exemple)
-- Note: Il est recommandé d'utiliser le bouton "Initialiser le compte Admin" sur login.html pour un vrai hash.
INSERT INTO "Users" ("Username", "PasswordHash") 
VALUES ('admin', '$2a$11$ev.7m.Qz/Tf7XmQyV1A9ue6p.X5Fz8E9B6p8X9v8Y7Z6x5w4v3u2t') 
ON CONFLICT DO NOTHING;
