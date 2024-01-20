# LineTen.TechnicalTask
Technical Test for LineTen Interview

# Requirements:
A sales company need to manage their products, customers and orders.
Please consider the following schema:

#### Customer
| Id | FirstName | LastName | Phone | Email |
|----|-----------|----------|-------|-------|

#### Product
| Id | Name | Description | SKU |
|----|------|-------------|-----|

#### Order
| ProductId | CustomerId | Status | CreatedDate | UpdatedDate |
|-----------|------------|--------|-------------|-------------|

- Create a .net solution that manages all three entities (CRUD).
- Create a github repository for your solution
- Follow Test Driven Development and demonstrate your approach with the history of commits
- Create unit and integration test projects and write tests using xunit
- Create a domain project and segregate your entities to demonstrate Domain Driven Development
- Create a data project and use Entity Framework and a code first approach to create your database
- Create a web api project to expose endpoints to manage all three entities
- Dockerise your solution

# Initiative Taken:
- I made the class libraries .net8, if desired we could always use .netstandard or multi-target if these will be consumed elsewhere
- For the sake of time, I am opting to not leave class/method Documentation Comments
- Opted to use the older paradigm for services Program.cs and Startup.cs due to static anaylsis issues with projet build and to make integration tests easier
- Opted to use a Data Mapper / ORM pattern for Repository layers, that way we could introduce any technology for the repository and the contract will still be met
- Opted to introduce an `Id` column to the Orders as I would argue the table design is weird, if you assume a composite primary key - Each customer could only order a product once!
- Noted that we can add Orders for customers and/or products when they don't exist - FKs a future enhancement!
- Originally planned to implement Data integration tests. Opted not to due to time constraints and as I have "kind of" achieved this with the inmemory based unit tests
- Introduced 2 additional projects: `*.Service.Domain` and `*.Service.Domain.Tests` so that I can reference the Response Models elsewhere
- Used Swagger UI so we have an interface to test with
- Opted to not use logger delegates and templates to save time
- Spent a long time trying to work out why any tests I wrote for foreign key violations, i.e. adding an order without the relevant customer or product do not error - Turns out the InMemory provider doesn't support them.

# Setup
Before running the Integration tests / Service locally you will need to install the following: 

## SQL Server Express (Local Db)
At the time of writing this was tested against SQL Server Express 2019 and Visual Studio 2022
![] https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16