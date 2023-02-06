# IbayQuery

**EN**

iBay ltd is a company which aim to produce the best experience for your online store experience.
Your task is to produce a full API using C# / .net Core to allow users to access information about products or other relevant data. This should also allow people to become seller and propose their own products.
All of the data have to be stored in an Sql Server database using Entity Framework Core.
The API needs to be REST compliant (method, endpoint …).
In addition, you have to create a simple .net console application to query the API.

**FR**

iBay ltd est une entreprise qui vise à produire la meilleure expérience pour votre boutique en ligne.
Votre tâche consiste à produire une API complète à l'aide de C# / .net Core pour permettre aux utilisateurs d'accéder aux informations sur les produits ou à d'autres données pertinentes. Cela devrait également permettre aux gens de devenir vendeur et de proposer leurs propres produits.
Toutes les données doivent être stockées dans une base de données Sql Server à l'aide d'Entity Framework Core.
L'API doit être compatible REST (méthode, endpoint …).
De plus, vous devez créer une application console .net simple pour interroger l'API.

# Required

- [![VS][VS]][VS-url]
- [![SQLSERVER][SQLSERVER]][SQLSERVER-url]

# Using

- [![C#][C#]][C#-url]
- [![.NET][.NET]][.NET-url]
- [![JWT][JWT]][JWT-url]

# Installation

## 1. Cloner le dépôt ou téléchargez-le en .zip

```
git clone https://github.com/MitryDim/IbayQuery.git
```
## 2. Ouvrer le fichier IbayQuery.sln.
>Vous devez possèdez la Version 2022 de Visual Studio pour avoir l'infrastructure .NET 7.0.

## 3. Création d'une base de données avec SQL Server.
>Il est possible de passer directement par Visual Studio en utilisant l'onglet Affichage => Explorateur d'objets SQL Server.


Afin de créer la base de données, il vous suffit de faire un **Clique Droit** sur le dossier **Bases de données** et ensuite, cliquer sur **Ajouter une nouvelle base de données**

![SQL SERVER](https://zupimages.net/up/23/06/b7wn.png)


Nommez la comme bon vous semble, cependant il est recommandé de la nommez **IBay**.

![SQL SERVER](https://zupimages.net/up/23/06/p62t.png)

Voila, votre Base de Données à été créer avec Succés.

![SQL SERVER](https://zupimages.net/up/23/06/z5mj.png)

## 4. Ajout des tables dans la base de données.
Tout d'abord, ouvrez le fichier IbayApi/**appsettings.json** et remplacez la Data Source de DBConnection par la votre.
Example : ```Data Source=(localdb)\MSSQLLocalDB;```.
Si vous avez choisis votre propre nom de Base de Données, définissez la à la place de ```Initial Catalog= IBay;```.

```
"DBConnection": "Data Source=MSI;Initial Catalog=IBay;Integrated Security=True;Trust Server Certificate=True"
```


Par la suite, rendez vous dans le Projet **Dal** grâce à la commande si dessous dans votre Onglet Powershell.

```
cd .\Dal\
```

Ensuite, exécutez les deux commandes si dessous :

Cette commande permet de créer un fichier de Migrations à partir du context de notre Base de données, vous pouvez renommer la Migration en modifiant '**Create**'.
```
dotnet ef --startup-project ..\IbayApi\IbayApi.csproj migrations add --context DatabaseContext 'Create'
```

Celle - ci nous permet d'utiliser le fichier de Migration afin d'update ou de créer les tables de la Base de Données si elles n'existent pas.
```
dotnet ef --startup-project ..\IbayApi\IbayApi.csproj database update
```

>Si vous avez une erreur suite à l'execution de l'une des deux commandes, vérifiez que vous possèdiez **dotnet ef**.


Pour l'installer :
```
dotnet tool install --global dotnet-ef
```

Pour l'update :
```
dotnet tool update --global dotnet-ef
```


Normalement, après une actualisation dans l'Explorateur d'objets SQL Server, vous devriez voir vos tables dans votre Base de Données.

# Lancement de la Solution

Afin d'utilisez l'Application Console, vous devriez au préalable démarrer le Projet IbayApi en **https**.

![Launch](https://zupimages.net/up/23/06/ebay.png)


>Toutes les fonctions de l'Application Console se trouvent dans IbayQuery/**Program.cs**



# Schéma de la Base de Données :

![SchemaBDD](https://zupimages.net/up/23/06/o49t.png)










[VS]: https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white
[VS-url]: https://visualstudio.microsoft.com/fr/

[SQLSERVER]: https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white
[SQLSERVER-url]: https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads

[C#]: https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white
[C#-url]: https://learn.microsoft.com/fr-fr/dotnet/csharp/

[.NET]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[.NET-url]: https://learn.microsoft.com/fr-fr/dotnet/

[JWT]:https://img.shields.io/badge/json%20web%20tokens-323330?style=for-the-badge&logo=json-web-tokens&logoColor=pink
[JWT-url]: https://jwt.io/introduction
