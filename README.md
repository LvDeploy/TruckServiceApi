# TruckServiceApi
Projeto em .NET Core para cadastro de Veículos

Este projeto foi feito com .NET Core 2.2

Features utilizadas:
EFCore
SwaggerUI
Moq for xUnit
AutoMapper
FluentValidator

<b> A documentação da Api foi configurada através do SwaggerGen </b>
<hr>
Atualmente possui apenas 4 endpoint:
/api/Truck/getAll
/api/Truck/insert
/api/Truck/update/{id}
/api/Truck/delete/{id}

<hr>
A pagina inicial do swagger está configurada para http://localhost:5000/swagger/index.html 

<br >

O projeto de inicialização chama-se TruckSystem.WebApi

<br >

Atualmente a connection string da aplicação aponta para um localdb, mas é possível modifica-la para outra instancia no SqlServer

Obs.:
Migrations já foram realizados e o update-database está configurado para rodar automaticamente através do program.cs quando o projeto for iniciado
