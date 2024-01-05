Linktree Clone API

This project is a clone of the Linktree website's API. The project uses ASP.NET Core Web API, MongoDB, and NOSQL technologies.


Technologies Used

ASP.NET Core Web API

MongoDB

NOSQL

Repository layers

Service layers

-------------------------------------------------------------------------------------------------------------------------------

Linktree Clone API

Bu proje, Linktree web sitesinin bir API klonudur. Proje, ASP.NET Core Web API, MongoDB ve NOSQL teknolojilerini kullanmaktadır.

Kullanılan teknolojiler

ASP.NET Core Web API

MongoDB

NOSQL

Repository mimari katmanı

Service katmanı

-------------------------------------------------------------------------------------------------------------------------------

![1](https://github.com/TkN42/Linktree-Clone---Api/assets/29886553/ed23a3b2-ba5b-4cda-9032-e0b656669e82)
![2](https://github.com/TkN42/Linktree-Clone---Api/assets/29886553/2d23547c-5382-46fa-9178-1dd2ed1d724a)
![3](https://github.com/TkN42/Linktree-Clone---Api/assets/29886553/ff248846-e37f-4368-9eba-1883586eb1b6)
![4](https://github.com/TkN42/Linktree-Clone---Api/assets/29886553/a2b05331-f378-472c-a0ea-ef6a73c6ccaf)
![db1](https://github.com/TkN42/Linktree-Clone---Api/assets/29886553/edb766af-81fe-471c-82ab-40811fd61eb5)
![db2](https://github.com/TkN42/Linktree-Clone---Api/assets/29886553/4023d6d1-298b-49f1-8910-45d4c68a5cc8)

-------------------------------------------------------------------------------------------------------------------------------

Don't forget to create appsettings.json to run the project.

An example json:

{
   "MongoDbConnection": {
     "ConnectionString": "mongodb://localhost:*****",
     "DatabaseName": "DBNAME"
   },
   "logging": {
     "LogLevel": {
       "Default": "Information",
       "Microsoft": "Warning",
       "Microsoft.Hosting.Lifetime": "Information"
     }
   },
   "AllowedHosts": "*"
}

-------------------------------------------------------------------------------------------------------------------------------

projeyi çalıştırmak için appsettings.json oluşturmayı unutmayın.

örnek bir json :

{
  "MongoDbConnection": {
    "ConnectionString": "mongodb://localhost:*****",
    "DatabaseName": "DBNAME"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
