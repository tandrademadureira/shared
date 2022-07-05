
# Shared

The services architecture created by MKT engineers must include concepts such as availability, scalability and performance.

The Shared library is a starting point for creating a new application from scratch. It allows us to apply design pattern best practices with a focus on code reuse, ease of maintenance and enabling more agile development.

## :sparkles: Key Features

>Note: Some of the key features may need the installation of a specific third party package to work.

- Authentication and Authorization
- CQRS
- Loging and Error Middlewares
- HealthCheck extension
- A lot of configurations, filters and helpers

## :books: How to use


There are a few ways to use the Shared library. The engineering team at MKT agreed that Shared would be used as a git sub-module of the repository for the application you will develop. The main purpose of using Shared this way is to debug code directly from your main repository.

To understand more about Git Submodule see:
https://git-scm.com/book/en/v2/Git-Tools-Submodules

If you've never used git submodule, let's go step-by-step:

First of all clone the repository in which you want to develop your brand new application.

```
git clone https://github.com/tandrademadureira/my-new-repo
```

Once this is done, just run the command to add the submodule pointing to the path you want in your project.

```
git submodule add https://github.com/tandrademadureira/shared.git shared/
```

>Note: When you clone a project that already has a submodule, the submodule directory will be there but empty. You must run two commands to initialize and fetch all the data from that project.
```
git submodule init
git submodule update
```

## Tech

MKT Shared library use technologies and design patterns to work properly and meet the required demand:

- [.Net 5] - [Reference](https://github.com/microsoft/dotnet)
- [EF Core] - [Reference](https://github.com/dotnet/efcore)
- [MediatR] - [Reference](https://github.com/jbogard/MediatR)
- [Serilog] - [Reference](https://github.com/serilog/serilog)
- [Swagger] - [Reference](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Newtonsoft Json] - [Reference](https://github.com/JamesNK/Newtonsoft.Json)

## License

**Under review, this repository is a strong candidate to be an open-source initiative!!**

## Todo

 - [x] Commit first readme version
 - [x] Set Microsoft Teams badge
 - [ ] Improve key feature itens
 - [ ] Set badge after creating CI/CD
 - [ ] Review license
