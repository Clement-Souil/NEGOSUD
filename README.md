# NEGOSUD - Application de gestion des commandes

## 📌 Description du projet
NEGOSUD est une application de gestion des commandes et des stocks pour une entreprise spécialisée dans la distribution de vins. Ce projet permet aux utilisateurs de :
- Créer, modifier et supprimer des commandes 🛒
- Gérer les stocks et les articles 📦
- Associer les commandes aux fournisseurs et aux employés 👤
- Générer un récapitulatif détaillé des commandes et des transactions 📊

L'application utilise **C# avec WPF (Windows Presentation Foundation)** pour l'interface utilisateur et **Entity Framework avec MySQL** pour la gestion des données.

---

## 🚀 Installation et configuration
### 1️⃣ **Cloner le projet**
```bash
git clone https://github.com/Clement-Souil/NEGOSUD.git
cd NEGOSUD
```

### 2️⃣ **Configuration de la base de données**
L'application utilise **MySQL**. Assure-toi d'avoir un serveur MySQL en local ou distant.

#### 📌 Créer la base de données
Dans MySQL, exécute la commande suivante :
```sql
CREATE DATABASE negosud_v2;
```

#### 📌 Configuration de la connexion
Dans `appsettings.json` (dans l'API), modifie la chaîne de connexion :
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=negosud_v2;User=root;Password=;"
}
```

### 3️⃣ **Exécuter les migrations**
```bash
dotnet ef database update
```

### 4️⃣ **Lancer le projet**
Dans Visual Studio, ouvre la solution et exécute :
- **L'API NEGOSUD** (`ApiNegosud`)
- **L'application cliente WPF** (`NEGOSUDClient`)

---

## 📂 Structure du projet
```bash
NEGOSUD/
│── ApiNegosud/       # API REST en ASP.NET Core
│── NEGOSUDClient/    # Application WPF (client)
│── NegosudLibrary/   # Bibliothèque partagée (DTO, DAO, services)
│── README.md         # Documentation du projet
```
### **📌 Composants principaux :**
- `ApiNegosud` : API REST qui gère les commandes, utilisateurs et fournisseurs.
- `NEGOSUDClient` : Interface utilisateur développée avec WPF et MVVM.
- `NegosudLibrary` : Bibliothèque contenant les **modèles DAO, DTO** et services partagés.

---

## ⚙️ Fonctionnalités principales
### **1️⃣ Gestion des commandes**
- Affichage des commandes en cours ✅
- Ajout de nouveaux produits à une commande ➕
- Validation et suppression de commandes ❌

### **2️⃣ Gestion des articles et des fournisseurs**
- Sélection d'articles par fournisseur 📦
- Filtrage des articles selon le fournisseur sélectionné 🔎
- Gestion des prix et quantités 💰

### **3️⃣ Interface utilisateur avec WPF et MVVM**
- **Utilisation du pattern MVVM** pour séparer la logique métier de l'UI.
- **Data Binding** et commandes pour gérer les interactions utilisateur.

---

## 🔥 API Endpoints
### **📌 Commandes**
| Méthode | Endpoint | Description |
|---------|---------|-------------|
| GET | `/api/commandes` | Récupère toutes les commandes |
| GET | `/api/commandes/{id}` | Récupère une commande spécifique |
| POST | `/api/commandes` | Crée une nouvelle commande |
| DELETE | `/api/commandes/{id}` | Supprime une commande |

### **📌 Articles**
| Méthode | Endpoint | Description |
|---------|---------|-------------|
| GET | `/api/articles` | Récupère tous les articles |
| GET | `/api/articles/{id}` | Récupère un article spécifique |
| POST | `/api/articles` | Ajoute un nouvel article |

---

## 🛠 Technologies utilisées
- **C# / .NET 6** - Backend et application cliente
- **WPF** - Interface utilisateur
- **Entity Framework Core** - ORM pour la gestion des données
- **MySQL** - Base de données
- **MVVM** - Architecture pour le client WPF

---

## 👥 Contributeurs
Projet réalisé par Clément Souil, Nathan Estay et Ilies Fernandez

---

## 📌 Améliorations futures
- 📈 Ajout de statistiques sur les ventes
- 📦 Gestion des livraisons
- 📊 Génération automatique de rapports
- 🌍 Support multilingue (FR/EN)

---

## 🎯 Conclusion
L’application **NEGOSUD** est un projet complet pour la gestion des commandes et des stocks, intégrant **une API REST et un client WPF**. Elle est conçue pour être **extensible et maintenable** grâce à **MVVM et Entity Framework**.

🚀 **N'hésite pas à contribuer et à proposer des améliorations !**

