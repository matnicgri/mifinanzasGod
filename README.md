# mifinanzasGod

- To run the project
Backend
    - open Visual studio solution Mifinanazas.God.Api.sln
    - open NugetManagerPackageConsole, select the project Mifinanazas.God.Persistence
    - execute the migracion Update-Database
    - the database should be created and populated the MoveOptions table

Front End
    -open VisualStudio code folder ..\mifinanzas.god.site
    -execute win cmd
    -cd ..\mifinanzas.god.site
    -ng serve
    -open the site localhost on the browser http://localhost:4200/

Configuration
    -in the front end the api backend  url is in src/enviroments/enviroment.ts for develop, for production the content is replaced for enviroment.prod.ts
    -the conection string for the database is in ../mifinanazas.god.api/Api/appsettings.json, for development the content sections existent in appsettings.Development.json ir replaced

Deploy
    -For Backend execute publish deploy for the Mifinanazas.God.Api project and copy the files to the server
    -For fornt end execute the cmd on ..\mifinanzas.god.site comand ng build --configuration=production and copy the files to the server
