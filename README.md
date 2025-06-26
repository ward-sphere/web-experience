# web-experience

CRUD service for my work experience and education.

## Usage

### Database Table Migration

This project was developed using Microsoft EF Core 9 Code-First. As such, any changes to our model should be reflected in the `Booger.Aids.Experience/Service/Migrations/` directory. When model changes are made (or seed data is desired), open VisualStudio and navigate to `Tools -> NuGet Package Manager -> Package Manager Console`. From there, run

```ps1
Add-Migration <migration-name>
```

to create your new migration. Find `Booger.Aids.Experience/Service/Migrations/<YYYY><MM><DD>*_<migration-name>.cs` and populate the `Up` and `Down` methods. Then, set the `PORTFOLIO_PGSQL_CONNECTION_STRING` value in your environment and again from the Package Manager Console run

```ps1
Update-Database
```

## References

* [Code First to a New Database](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database)
* [READMINE: Suggested template for software READMEs](https://github.com/mhucka/readmine)