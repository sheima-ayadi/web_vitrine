CREATE DATABASE IngeniusIT_DB;
GO

USE IngeniusIT_DB;
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL
);

CREATE TABLE BlogPosts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Tag NVARCHAR(100),
    Image NVARCHAR(MAX),
    Date NVARCHAR(50)
);

-- Note: Le premier utilisateur sera créé via l'endpoint /api/Auth/setup
