# web-experience

CRUD service for my work experience and education.

## Usage

### Database Table Migration

This project was developed using Microsoft EF Core 9 Code-First. As such, any changes to our model should be reflected in the `Booger.Aids.Experience/Service/Migrations/` directory. When model changes are made (or seed data is desired), open VisualStudio and navigate to `Tools -> NuGet Package Manager -> Package Manager Console`. From there, run

```ps1
Add-Migration <migration-name>
```

to create your new migration. Find `Booger.Aids.Experience/Service/Migrations/<YYYY><MM><DD>*_<migration-name>.cs` and populate the `Up` and `Down` methods. Then, set the `AIDSBOOGER__PORTFOLIO__EXPERIENCE__CONNSTR` value in your environment and again from the Package Manager Console run

```ps1
Update-Database
```

### Docker

Trunk-based releases using Docker.

Build (from `.sln` directory):

```bash
docker build -t portfolio/experience -t portfolio/experience:$(date +%Y.%m.%d).${BUILD} -f $(pwd)/Service/Dockerfile .
aws ecr get-login-password --region us-east-2 | docker login --username AWS --password-stdin 505499692157.dkr.ecr.us-east-2.amazonaws.com
docker tag portfolio/experience:$(date +%Y.%m.%d).${BUILD} 505499692157.dkr.ecr.us-east-2.amazonaws.com/portfolio/experience:$(date +%Y.%m.%d).${BUILD}
docker push 505499692157.dkr.ecr.us-east-2.amazonaws.com/portfolio/experience:$(date +%Y.%m.%d).${BUILD}
```

## References

* [Code First to a New Database](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database)
* [READMINE: Suggested template for software READMEs](https://github.com/mhucka/readmine)