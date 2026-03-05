# CryptoApp - Blazor WebAssembly 🚀

Cette application a été développée dans le cadre d'un examen de Programmation Orientée Objet. Elle permet d'afficher les données en temps réel du marché des cryptomonnaies et offre aux utilisateurs authentifiés la possibilité de gérer leurs favoris localement.

## 🌟 Fonctionnalités principales

- **Consommation d'API REST :** Récupération et affichage du Top 10 des cryptomonnaies via l'API publique de CoinGecko. Gestion des erreurs (ex: limite de requêtes API / Erreur 429) intégrée pour éviter les crashs.
- **Authentification locale :** Mise en place d'un fournisseur d'état d'authentification personnalisé (`FakeAuthStateProvider`) utilisant les composants officiels de Blazor, permettant de simuler une connexion sécurisée sans nécessiter de backend lourd.
- **Gestion des Favoris (CRUD) :** Les utilisateurs connectés peuvent ajouter, modifier (changer le prix), et supprimer des cryptomonnaies de leurs favoris.
- **Persistance des données :** Les favoris sont sauvegardés de manière persistante dans le `LocalStorage` du navigateur de l'utilisateur via `IJSRuntime`.
- **Surcharge des données :** Lorsqu'un utilisateur est connecté, les prix qu'il a modifiés dans ses favoris remplacent visuellement les prix réels de l'API sur la page d'accueil.

## 🛠️ Technologies utilisées

- **Framework :** .NET 10 / Blazor WebAssembly
- **Langage :** C# / HTML / CSS (Bootstrap)
- **Stockage :** LocalStorage (Navigateur Web)
- **Tests :** xUnit (Tests unitaires) & bUnit (Tests de composants/procéduraux)

## 📁 Structure du projet

- `/Models` : Modèles de données (ex: `CryptoCoin.cs`) mappés sur le JSON de l'API.
- `/Services` : Logique métier de l'application.
  - `CryptoService` : Appels HTTP vers l'API CoinGecko.
  - `FavoriteService` : Interopérabilité JavaScript pour lire/écrire dans le LocalStorage.
  - `FakeAuthStateProvider` : Gestion de la session utilisateur locale.
- `/Pages` : Vues de l'application (`Home.razor`, `Favorites.razor`).
- `/CryptoApp.Tests` : Projet séparé contenant les tests automatisés.

## 🚀 Installation et exécution

### Prérequis
- [.NET 10 SDK](https://dotnet.microsoft.com/download) installé sur la machine.

### Lancer l'application
1. Ouvrir un terminal dans le dossier racine du projet `CryptoApp`.
2. Exécuter la commande suivante :
   ```bash
   dotnet watch