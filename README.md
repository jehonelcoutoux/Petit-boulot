🧰 P’tiBoulot

Plateforme desktop de mise en relation entre clients et prestataires de services locaux au Gabon
P’tiBoulot est une application desktop développée en C# .NET 8 avec WPF, permettant de connecter facilement des clients à des prestataires de services locaux.
L’application propose une gestion complète des offres, candidatures, notifications et tableaux de bord adaptés selon les rôles utilisateurs.


Objectif du projet
L’objectif de P’tiBoulot est de digitaliser les petits services locaux au Gabon en proposant une plateforme simple, intuitive et sécurisée permettant :
Aux clients de publier des besoins et trouver des prestataires
Aux prestataires de proposer leurs services et répondre aux offres
Aux administrateurs de superviser l’activité de la plateforme

Technologies utilisées
Technologie
Description
C# .NET 8
Développement principal
WPF
Interface utilisateur desktop
SQL Server LocalDB
Base de données
Entity Framework Core
ORM pour l’accès aux données
BCrypt
Sécurisation des mots de passe
MVVM
Architecture du projet


 Architecture du projet
Le projet suit une architecture MVVM (Model - View - ViewModel) afin de garantir :
Une séparation claire des responsabilités
Une meilleure maintenabilité
Une facilité d’évolution du projet
Structure générale
Bash

PtiBoulot/
│
├── Models/
├── Views/
├── ViewModels/
├── Services/
├── Data/
├── Styles/
├── Assets/
└── App.xaml


 Gestion des rôles
L’application comporte 3 rôles principaux :
Rôle
Fonction
Client
Publie des offres et recherche des prestataires
Prestataire
Consulte les offres et postule
Administrateur
Gère l’ensemble de la plateforme

 
 Fonctionnalités principales

 Authentification sécurisée
Connexion utilisateur
Inscription en ligne
Gestion des rôles
Hachage des mots de passe avec BCrypt
 
Tableaux de bord dynamiques
Chaque utilisateur dispose d’un tableau de bord personnalisé :
Statistiques en temps réel
Accès rapide aux fonctionnalités
Interface adaptée au rôle

 Gestion des offres
Les clients peuvent :
Publier des missions
Modifier les offres
Suivre les candidatures
Les prestataires peuvent :
Rechercher des offres
Postuler aux missions
Suivre leur statut

 Annuaire des prestataires
Consultation des profils
Affichage des compétences
Tarification des services
Recherche instantanée

 Notifications
Alertes en temps réel
Historique des notifications
Badge compteur

 Suivi des missions
Acceptation/refus des candidatures
Gestion du cycle des missions
Système de notation et avis

 Base de données
Le projet utilise SQL Server LocalDB avec Entity Framework Core.
Tables principales
Table
Description
Utilisateurs
Gestion des comptes
Offres
Missions publiées
Candidatures
Réponses des prestataires
Notifications
Alertes système


Installation du projet

1️⃣ Cloner le projet
Bash
git clone https://github.com/votre-utilisateur/ptiboulot.git

2️⃣ Ouvrir le projet
Ouvrir la solution avec :
Bash
Visual Studio 2022

3️⃣ Configurer la base de données
Modifier la chaîne de connexion dans :
JSON
appsettings.json
ou dans :
C#
AppDbContext.cs

4️⃣ Appliquer les migrations
Dans la console du gestionnaire de package :
PowerShell
Update-Database

5️⃣ Lancer l’application
Bash
Ctrl + F5

 
 Difficultés rencontrées

 Navigation entre les pages WPF
Problème
Le Frame WPF ne conservait pas correctement l’état des pages.
Solution
Utilisation d’un système de navigation dynamique avec :
NavigationService
Sidebar pilotée par RadioButton

 Requêtes EF Core complexes
Problème
Certaines propriétés UI n’étaient pas traduisibles en SQL.
Solution
Séparation du traitement :
Chargement des données avec .ToList()
Mapping en mémoire

 Gestion des rôles
Problème
Éviter la duplication des interfaces selon les rôles.
Solution
Injection dynamique des vues selon :
C#
Session.UtilisateurConnecte.Role

 Gestion des mots de passe
Problème
Coexistence de mots de passe hashés et non hashés.
Solution
try/catch autour de BCrypt.Verify()
Migration progressive vers BCrypt

 Perspectives d’évolution

Messagerie instantanée
Intégration d’un chat temps réel avec SignalR.

 Paiement intégré
Support Mobile Money et carte bancaire.

 Application mobile
Version Android/iOS avec .NET MAUI.

 Géolocalisation
Localisation des prestataires proches.

 Dashboard analytique
Statistiques avancées pour l’administration.

 Recommandation IA
Matching intelligent entre clients et prestataires.

 Aperçus de l’application
Interface Client
Interface Prestataire
Interface Administrateur

 Auteur
Fred G. COUTOUX B. et Antoine EMBO NZUE
Étudiant en ingénierie informatique
Libreville, Gabon

 Licence
Projet académique et démonstratif.
Utilisation libre à des fins éducatives et de démonstration.
⭐ Remerciements
Merci à toutes les personnes ayant contribué au développement et aux tests de l’application P’tiBoulot.
