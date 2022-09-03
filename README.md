Introduction 

This tutorial is Part of the Step by Step Series: How to build a Clean Web API from the basics of ASP.NET Core.

We are starting building a Customer Web API with Net Core 6
using the native dependency injection provided on this framework and creating a repository pattern taking advantage of the [entity framework](https://docs.microsoft.com/en-us/ef/core/) and one of the most used nudget packages, [automapper](https://docs.automapper.org/en/stable/) following the Solid Principles. 

You may need to research this on the web if you don't know the SOLID principle.

## Why should I use this?  


**Dependency Injection**


In short, dependency injection is a design pattern in which an object receives other objects. A form of inversion of control, dependency injection aims to separate the concerns of constructing objects and using them, leading to loosely coupled programs.


Some of the problems that you resolve are these:  
    
- The use of an interface or base class to abstract the dependency implementation.
- Registration of the dependency in a service container. ASP.NET Core provides a built-in service container.
- Injection of the service into the class's constructor where it has to be used. The framework takes on the responsibility of creating an instance of the dependency and disposing of it when it's no longer needed.

See more details about this [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0)


**Repository pattern**

According to Martin Flower A repository mediates between the domain and data mapping layers, acting like an in-memory domain object collection. Client objects construct query specifications declaratively and submit them to the repository for satisfaction. 

See the details [here](https://martinfowler.com/eaaCatalog/repository.html).

![Image description](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/ltr9xi52l2e8hpf7cyym.png)

**AutoMappter**

Automapper is a simple library that helps us to transform one object type into another. It is a convention-based object-to-object mapper that requires minimal configuration. 

What problems will resolve automapper?

One of the common uses is when we have to map one object type to another. It is boring to do it every time we have to do it, so this simple tool makes it for us.

See more details [here](https://automapper.org)


## SQL Sintaxt for SQL Database 

```SQL
CREATE DATABASE CustomerDemoDb 
go

Use CustomerDemoDb
  
CREATE TABLE [dbo].[Customer]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
    [FirstName] NVARCHAR (150) NULL,
    [LastName] NVARCHAR (150) NULL,
    [EmailAddress] NVARCHAR (150) NULL,
    [DateOfBirth] DATE NULL,
    [CreatedAt] DATE NULL
);

```


